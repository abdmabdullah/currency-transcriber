using CurrencyTranscriberApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CurrencyTranscriberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        [Route("GetTranscribedCurrency")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromQuery] string number)
        {
            if (number.Contains("."))
                return BadRequest("invalid format");

            number = Regex.Replace(number, @"\s", "");

            decimal amount;

            if (!decimal.TryParse(number.Replace(",", "."), out amount))
                return BadRequest("Invalid format");

            if (amount < 0 || amount > 999999999.99M)
                return BadRequest("Please enter an amount between 0 and 999999999,99");

            string[] splitNumber = number.Split(CurrencyHelper.DECIMAL_SEPARATOR);

            int dollars = int.Parse(splitNumber[0]);
            int cents = splitNumber.Length > 1 ? string.IsNullOrWhiteSpace(splitNumber[1]) ? 0 : int.Parse(splitNumber[1]) : 0;

            if (cents == 1)
                cents = 10;

            if (cents > 99)
                return BadRequest("Invalid cents");

            string dollarWords = CurrencyHelper.GetWords(dollars);
            string centWords = CurrencyHelper.GetWords(cents);

            string currencyInWords;

            if (!string.IsNullOrWhiteSpace(centWords) && cents > 0)
                currencyInWords = CurrencyHelper.GetFormattedCurrencyInWords(dollarWords, centWords);
            else
                currencyInWords = CurrencyHelper.GetFormattedCurrencyInWords(dollarWords);

            return Ok(currencyInWords);
        }
    }
}
