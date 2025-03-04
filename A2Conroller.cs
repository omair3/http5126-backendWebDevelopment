using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace C_A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class A2Conroller : ControllerBase
    {
        /// <summary>
        /// The method  calculates the final score in the game Deliv-e-droid based on the given point system. The method calculates the score determined by the number of packages delivered, collisions with obstacles, and the bonus condition
        /// </summary>
        /// <param name="Deliveries">Deliveries value in Int</param>
        /// <param name="Collisions">Collision value in Int</param>
        /// <returns>Returns who is taller</returns>
        /// <example>
        /// POST: localhost:7005/api/J1/Delivedroid
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: Collisions=2&Deliveries=5
        /// -> 730
        /// POST:localhost:7005/api/J1/Delivedroid
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: Collisions=10&Deliveries=0
        /// -> -100
        /// POST: localhost:7005/api/J1/Delivedroid
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: Collisions=3&Deliveries=2
        /// -> 70
        /// </example>
        [HttpPost(template:"/api/J1/Delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]
        public int Delivedroid([FromForm]int Collisions, [FromForm]int Deliveries)
        {
            int total=0;
            if ( Deliveries>Collisions)
            {
                total = total + 500;
            }
            if (Deliveries > 0)
            {
                total=total+ (Deliveries * 50);
                
            }
            if (Collisions > 0)
            {
                total = total - (Collisions * 10);
            }

            return total;
        }
        /// <summary>
        /// This method tkes a  chili pepper names as input and calculates a total spiciness score based on predefined Scoville heat values.
        /// </summary>
        /// <returns>Total spiciness score based on heat values</returns>
        /// <example>
        /// <param name="ingredients">List of names of peppers</param>
        /// GET localhost:7005/api/J2/ChiliPeppers&Ingredients=Poblano,Cayenne,Thai,Poblano-> 1180000
        /// </example>
        ///  <example>
        /// GET localhost:7005/api/J2/ChiliPeppers&Ingredients=Habanero,Habanero,Habanero,Habanero,Habanero-> 625000
        /// </example>
        ///  <example>
        /// GET localhost:7005/api/J2/ChiliPeppers&Ingredients=Poblano,Mirasol,Serrano,Cayenne,Thai,Habanero,Serrano-> 278500
        /// </example>
        [HttpGet(template: "/api/J2/ChiliPeppers")]
        public int J3(string ingredients)
        {
           string[] values = ingredients.Split(',');

            Dictionary<string, int> value = new Dictionary<string, int>()
            {
                {"Poblano",1500},
                {"Mirasol",6000},
                {"Serrano",15500},
                {"Cayenne",40000},
                {"Thai",75000},
                {"Habanero",125000}
            };
            int total = 0;
            for(int i = 0; i < values.Length; i++)
            {
                foreach (KeyValuePair<string,int> name in value)
                {
                    if (values[i].Contains(name.Key))
                    {
                        total = total + name.Value;
                    }
                }
           
            }
            return total;
        }
    
     /// <summary>
        /// Computes the total cost of sushi plates based on an arithmetic expression.
        /// The expression calculates the cost by multiplying the number of plates of each color
        /// with their respective prices and summing the results.
        /// </summary>
        /// <param name="red">Number of red plates ($3 each)</param>
        /// <param name="green">Number of green plates ($4 each)</param>
        /// <param name="blue">Number of blue plates ($5 each)</param>
        /// <returns>Total cost in dollars</returns>
        ///  <example>
        /// POST:localhost:7005/api/J1/SushiCost
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: red=7&green=3&blue=8
        /// -> 73
        /// POST: localhost:7005/api/J1/SushiCost
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: red=0&green=2&blue=4
        /// -> 28
        ///  </example>
        [HttpPost(template: "/api/J1/SushiCost")]
        [Consumes("application/x-www-form-urlencoded")]
        public int SushiCost([FromForm] int red, [FromForm] int green, [FromForm] int blue)
        {
            int redplate = 3;
            int greenplate = 4;
            int blueplate = 5;
            int total= (red*redplate) + (green*greenplate) + (blue*blueplate);
            return total;

        }
        /// <summary>
        /// Calculates the final size of Dusa after encountering Yobis.
        /// Dusa eats Yobis smaller than itself and absorbs their size.
        /// If a Yobi is the same size or larger, Dusa runs away.
        /// </summary>
        /// <param name="initialSize">Dusa's initial size</param>
        /// <param name="yobiSizes">List of Yobi sizes</param>
        /// <returns>Final size of Dusa</returns>
        /// ///  <example>
        /// POST:localhost:7005/api/J2/DusaYobis
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: yobiSizes=3&yobiSizes=2&yobiSizes=9&yobiSizes=20&yobiSizes=22&yobiSizes=14&initialSize=5
        /// -> 19
        /// POST: localhost:7005/api/J2/DusaYobis
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: yobiSizes=10&yobiSizes=3&yobiSizes=5&initialSize=10
        /// -> 10
        ///  </example>
        [HttpPost(template: "/api/J2/DusaYobis")]
        [Consumes("application/x-www-form-urlencoded")]
        public int DusaYobis([FromForm] List<int> yobiSizes, [FromForm] int initialSize)
        {
            int dusaSize = initialSize;
            foreach (int yobi in yobiSizes)
            {
                if (yobi < dusaSize)
                {
                    dusaSize += yobi; // Absorb Yobi
                }
                else
                {
                    break; // Dusa runs away
                }
            }
            return dusaSize;
        }
        /// <summary>
        /// Determines the bronze score and the number of participants achieving that score.
        /// The bronze score is the third-highest distinct score among participants, and the count is the number of participants achieving it.
        /// </summary>
        /// <param name="scores">List of participant scores</param>
        /// <returns>A tuple containing the bronze score and the number of participants with that score</returns>
        /// <example>
        /// POST: localhost:7005/api/J3/bronzeScores
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: scores=4&scores=70&scores=62&scores=58&scores=73
        /// -> 62 1
        /// POST: localhost:7005/api/J3/bronzeScores
        /// Header: Content-Type: application/x-www-formurlencoded
        /// POST DATA: scores=8&scores=75&scores=70&scores=60&scores=70&scores=70&scores=60&scores=75&scores=70
        /// -> 60 2
        /// </example>
        [HttpPost(template: "/api/J3/bronzeScores")]
        [Consumes("application/x-www-form-urlencoded")]
        public string bronzeScores([FromForm]List<int> scores)
        {

            
            List<int> distinctScores = new List<int>();

            
            foreach (int score in scores)
            {
                if (!distinctScores.Contains(score))
                {
                    distinctScores.Add(score);
                }
            }

            
            for (int i = 0; i < distinctScores.Count - 1; i++)
            {
                for (int j = i + 1; j < distinctScores.Count; j++)
                {
                    if (distinctScores[i] < distinctScores[j])
                    {
                        
                        int temp = distinctScores[i];
                        distinctScores[i] = distinctScores[j];
                        distinctScores[j] = temp;
                    }
                }
            }

            
            int bronzeScore = distinctScores[2];

            
            int bronzeCount = 0;
            foreach (int score in scores)
            {
                if (score == bronzeScore)
                {
                    bronzeCount++;
                }
            }

            
            return bronzeScore + " " + bronzeCount;
        }




    }
}


