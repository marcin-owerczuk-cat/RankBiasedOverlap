using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace RboWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RboController : ControllerBase
    {
        [HttpPost]
        public decimal Post([FromBody] Ranks ranks) => Rbo.Rbo.GetScore(ranks.a, ranks.b, ranks.p);
    }


    public class Ranks
    {
        public List<int> a { get; set; }
        public List<int> b { get; set; }
        public decimal p { get; set; }
    }
}