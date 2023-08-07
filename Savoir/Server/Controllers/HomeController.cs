using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Savoir.DataAccess;
using Savoir.Models;
using Savoir.Server.Helper;
using Savoir.Server.Resources;
using System.Data;
using System.Globalization;

namespace Savoir.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetCashFlow")]
        public async Task<IActionResult> GetCashFlow()
        {
            return Ok(await _context.CashFlows.ToListAsync());
        }
        [HttpGet("GetInvestment")]
        public async Task<IActionResult> GetInvestment(int userId)
        {
            //var result =
            //    from inv in await _context.Investments.Where(x => x.Status == 'A').ToArrayAsync()
            //    join
            //        lef in await _context.LendingPlatforms.ToListAsync() on inv.LendingPlatformId equals lef.LendingPlatformId
            //    join
            //        ler in await _context.LendingPlatformRates.ToListAsync() on inv.LendingPlatformRateId equals ler.LendingPlatformRateId
            //    join
            //        invT in await _context.InvestmentTerms.ToListAsync() on inv.InvestmentTermId equals invT.TermId
            //    select new
            //    {
            //        Exchange = lef.Name,
            //        DepositRate = ler.ReturnCurrencyRate1,
            //        Term = invT.InvestmentTermDescription,
            //        StartDate = inv.StartingDate,
            //        EndDate = inv.EndDate,
            //        MaturityDate = inv.MaturityDate,
            //        InvestedAmount = inv.Amount,
            //        ReturnAmount = 0
            //    };
            //return Ok(result);

            using (var cn = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync();
                var pagination = new Pagination();
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var result = await
                    cn.QueryAsync("sp_get_investment", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }

        }
        [HttpGet("GetMarketsOverView")]
        public async Task<IActionResult> GetMarketsOverView()
        {
            var result =
                from
                    lef in await _context.LendingPlatforms.ToListAsync()
                join
                    ler in await _context.LendingPlatformRates.OrderByDescending(x => x.Date).ToListAsync() on lef.LendingPlatformId equals ler.LendingPlatformId
                join
                    invT in await _context.InvestmentTerms.ToListAsync() on ler.TermId equals invT.TermId
                select new
                {
                    LendingPlatformId = lef.LendingPlatformId,
                    LendingPlatformRateId = ler.LendingPlatformRateId,
                    TermId = invT.TermId,
                    Exchange = lef.Name,
                    DepositRate = ler.ReturnCurrencyRate1,
                    Term = invT.InvestmentTermDescription
                };
            return Ok(result);
        }
        [HttpGet("GetPastTrades")]
        public async Task<IActionResult> GetPastTrades(int userId)
        {
            //var result =
            //     from inv in await _context.Investments.Where(x => x.Status == 'M').ToArrayAsync()
            //     join
            //         lef in await _context.LendingPlatforms.ToListAsync() on inv.LendingPlatformId equals lef.LendingPlatformId
            //     join
            //         ler in await _context.LendingPlatformRates.ToListAsync() on inv.LendingPlatformRateId equals ler.LendingPlatformRateId
            //     join
            //         invT in await _context.InvestmentTerms.ToListAsync() on inv.InvestmentTermId equals invT.TermId
            //     select new
            //     {
            //         Exchange = lef.Name,
            //         DepositRate = ler.ReturnCurrencyRate1,
            //         Term = invT.InvestmentTermDescription,
            //         StartDate = inv.StartingDate,
            //         EndDate = inv.EndDate,
            //         MaturityDate = inv.MaturityDate,
            //         InvestedAmount = inv.Amount,
            //         ReturnAmount = 0
            //     };
            //return Ok(result);
            using (var cn = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var result = await
                    cn.QueryAsync("sp_get_past_trades", parameters, commandType: CommandType.StoredProcedure);
                return Ok(result);
            }
        }
        [HttpGet("GetInvestmentTerms")]
        public async Task<IActionResult> GetInvestmentTerms()
        {
            return Ok(await _context.InvestmentTerms.ToListAsync());
        }
        [HttpGet("OrganizationCashForecast")]
        public async Task<IActionResult> OrganizationCashForecast(int userId)
        {
            //var companies = await _context.Companies.ToListAsync();
            var companies = new List<Company>();
            using (var cn = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                companies = (await cn.QueryAsync<Company>("sp_get_user_wise_companies", parameters, commandType: CommandType.StoredProcedure)).ToList();
            }
            var cashFlows = await _context.CashFlows.ToListAsync();
            var result = from OG in await _context.Organizations.ToListAsync()
                         join CU in await _context.Currency.ToListAsync() on OG.DefaultCurrencyId equals CU.CurrencyId
                         join CF in await _context.CashFlows.ToListAsync() on OG.OrganizationId equals CF.OrganizationId
                         group CF by new { OG.OrganizationId, OG.Name, OG.BankAccount, CU.CurrencyCode, CU.CurrencyId } into GB
                         select new
                         {
                             OrganizationName = GB.Key.Name,
                             BankAccount = GB.Key.BankAccount,
                             Currency = GB.Key.CurrencyCode,
                             NetCashFlow = GB.Sum(x => x.NetCashFlow),
                             CompanyWise = from CM in companies
                                           join CF in cashFlows on CM.CompanyId equals CF.CompanyId
                                           where CM.OrganizationId == GB.Key.OrganizationId
                                           group CF by new { CM.CompanyId,CM.CompanyName, CM.BankAccount, CF.Currency } into SGB
                                           select new
                                           {
                                               OrganizationId =  GB.Key.OrganizationId,
                                               OrganiaztionName = GB.Key.Name,
                                               CompanyId = SGB.Key.CompanyId,
                                               CompanyName = SGB.Key.CompanyName,
                                               BankAccount = SGB.Key.BankAccount,
                                               NetCashFlow = SGB.Sum(x => x.NetCashFlow),
                                               CurrencyId = GB.Key.CurrencyId,
                                               Currency = SGB.Key.Currency
                                           }
                         };
            return Ok(result.FirstOrDefault());
        }
        [HttpPost("CreateInvestment")]
        public async Task<IActionResult> CreateInvestment(InvestmentModel InvestmentModel)
        {
            var startingDate = DateTime.Now;
            var investmentTerm = await _context.InvestmentTerms.Where(i => i.TermId == InvestmentModel.InvestmentTermId).FirstAsync();
            var maturityDate =startingDate.AddDays(investmentTerm.Term);
            var endDate = maturityDate;
            var status = 'A';
            var investment = new Investment()
            {
                OrganizationId = InvestmentModel.OrganizationId,
                CompanyId = InvestmentModel.CompanyId,
                LendingPlatformId = InvestmentModel.LendingPlatformId,
                LendingPlatformRateId = InvestmentModel.LendingPlatformRateId,
                InvestmentTermId = InvestmentModel.InvestmentTermId,
                StartingDate = startingDate,
                MaturityDate = maturityDate,
                EndDate = endDate,
                Amount = InvestmentModel.Amount,
                Currency = InvestmentModel.Currency,
                Status = status
            };
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetCompanyWiseCashForecast")]
        public async Task<IActionResult> GetCompanyWiseCashForecast(int CompanyId)
        {
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddMonths(5);
            var monthsYears = MonthsBetween(startDate, endDate);
            var ON = from MY in monthsYears
                               join OA in await _context.OperatingActivities.ToListAsync() on new { MY.Year, MY.Month, CompanyId } equals new { OA.Year, OA.Month, OA.CompanyId } into GOA
                               from SGOA in GOA.DefaultIfEmpty()
                               orderby MY.Year, MY.Month 
                               select new
                               {
                                   Year = MY.Year,
                                   Month = MY.Month,
                                   MonthYear = MY.MonthYear,
                                   Amount = SGOA?.NetCashflowFromOA ?? decimal.Zero,
                               };
            var operatingNet = new { Serial = 1, Reason = "Operating Ex Net",Expence = ON.DistinctBy(x => x.MonthYear)};
            var IN = from MY in monthsYears
                               join IA in await _context.InvestmentActivities.ToListAsync() on new { MY.Year, MY.Month, CompanyId } equals new { IA.Year, IA.Month, IA.CompanyId } into GIA
                               from SGIA in GIA.DefaultIfEmpty()
                               orderby MY.Year, MY.Month
                               select new
                               {
                                   Year = MY.Year,
                                   Month = MY.Month,
                                   MonthYear = MY.MonthYear,
                                   Amount = SGIA?.NetCashfromInvestmentActivities ?? decimal.Zero,
                               };
            var investingNet = new { Serial = 2, Reason = "Investing Net", Expence = IN.DistinctBy(x => x.MonthYear) };
            var FN = from MY in monthsYears
                               join FA in await _context.FinancialActivities.ToListAsync() on new { MY.Year, MY.Month, CompanyId } equals new { FA.Year, FA.Month, FA.CompanyId } into GFA
                               from SGFA in GFA.DefaultIfEmpty()
                               orderby MY.Year, MY.Month
                               select new
                               {
                                   Year = MY.Year,
                                   Month = MY.Month,
                                   MonthYear = MY.MonthYear,
                                   Amount = SGFA?.NetCashFromFinancialActivities ?? decimal.Zero,
                               };
            var financialNet = new { Serial = 3, Reason = "Financing Net", Expence = FN.DistinctBy(x => x.MonthYear) };
            var CP = from MY in monthsYears
                                  join OB in await _context.OpeningBalance.ToListAsync() on new { MY.Year, MY.Month, CompanyId } equals new { OB.Year, OB.Month, OB.CompanyId } into GOB
                                  from SGOB in GOB.DefaultIfEmpty()
                                  orderby MY.Year, MY.Month
                                  select new
                                  {
                                      Year = MY.Year,
                                      Month = MY.Month,
                                      MonthYear = MY.MonthYear,
                                      Amount = SGOB?.Balance ?? decimal.Zero,
                                  };
            var cashEndOfPeriod = new { Serial = 4, Reason = "Cash End of Period", Expence = CP.Distinct() };
            var result = new List<object>();
            result.Add(operatingNet);
            result.Add(investingNet);
            result.Add(financialNet);
            result.Add(cashEndOfPeriod);
            return Ok(result);
        }
        public static IEnumerable<(int Month, int Year, string MonthYear)> MonthsBetween(
       DateTime startDate,
       DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }
            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                yield return (
                    iterator.Month,
                    iterator.Year,
                    dateTimeFormat.GetAbbreviatedMonthName(iterator.Month).ToUpper() + iterator.ToString("yy")
                );

                iterator = iterator.AddMonths(1);
            }
        }
    }
}
