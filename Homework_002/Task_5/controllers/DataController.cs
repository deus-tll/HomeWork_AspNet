using Microsoft.AspNetCore.Mvc;
using Task_5.models;


namespace Task_5.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(GetCountries());
        }
        
        private static List<Country> GetCountries()
        {
            string ukraineDesc = "Ukraine is a country in Eastern Europe. It is the second-largest European country after Russia, " +
                "which it borders to the east and northeast. It is also bordered by Belarus to the north; by Poland, Slovakia, and " +
                "Hungary to the west; and by Romania and Moldova to the southwest; with a coastline along the Black Sea and the Sea " +
                "of Azov to the south and southeast. Kyiv is the nation's capital and largest city, followed by Kharkiv, Dnipro and " +
                "Odesa. Ukraine's official language is Ukrainian; Russian is also widely spoken, especially in the east and south.";

            string japanDesc = "Japan is an island country in East Asia. It is situated in the northwest Pacific Ocean and is bordered " +
                "on the west by the Sea of Japan, extending from the Sea of Okhotsk in the north toward the East China Sea, Philippine " +
                "Sea, and Taiwan in the south. Japan is a part of the Ring of Fire, and spans an archipelago of 14,125 islands, with the " +
                "five main islands being Hokkaido, Honshu (the \"mainland\"), Shikoku, Kyushu, and Okinawa. Tokyo is the nation's capital " +
                "and largest city, followed by Yokohama, Osaka, Nagoya, Sapporo, Fukuoka, Kobe, and Kyoto.";

            string usaDesc = "The United States of America (U.S.A. or USA), commonly known as the United States (U.S. or US) or America, " +
                "is a country, primarily located in North America, that consists of 50 states, a federal district, five major unincorporated " +
                "territories, nine Minor Outlying Islands, and 326 Indian reservations. It is the world's third-largest country by both land " +
                "and total area. It shares land borders with Canada to its north and with Mexico to its south and has maritime borders with " +
                "the Bahamas, Cuba, Russia, and other nations. With a population of over 333 million, it is the most populous country in the " +
                "Americas and the third-most populous in the world. The national capital of the United States is Washington, D.C., and its " +
                "most populous city and principal financial center is New York City.";

            string ukDesc = "The United Kingdom of Great Britain and Northern Ireland, simply known as the United Kingdom (UK) or Britain, " +
                "is a country in Northwestern Europe, off the north-western coast of the continental mainland. It comprises England, Scotland, " +
                "Wales, and Northern Ireland. It includes the island of Great Britain, the north-eastern part of the island of Ireland, and " +
                "most of the smaller islands within the British Isles. Northern Ireland shares a land border with the Republic of Ireland; " +
                "otherwise, the United Kingdom is surrounded by the Atlantic Ocean, the North Sea, the English Channel, the Celtic Sea and " +
                "the Irish Sea. The total area of the United Kingdom is 93,628 square miles (242,495 km2), with an estimated 2023 population " +
                "of over 68 million people.";

            string greeceDesc = "Greece, officially the Hellenic Republic, is a country in Southeast Europe, situated on the southern tip of " +
                "the Balkan peninsula. Greece shares land borders with Albania to the northwest, North Macedonia and Bulgaria to the north, " +
                "and Turkey to the east. The Aegean Sea lies to the east of the mainland, the Ionian Sea to the west, and the Sea of Crete and " +
                "the Mediterranean Sea to the south. Greece has the longest coastline on the Mediterranean Basin, featuring thousands of islands. " +
                "The country consists of nine traditional geographic regions, and has a population of approximately 10.5 million. Athens is the " +
                "nation's capital and largest city, followed by Thessaloniki and Patras.";

            List<Country> countries = new()
            {
                new Country { Id = 1,
                              Name = "Ukraine",
                              Description = ukraineDesc,
                              ImageBase64 = ConvertImageToBase64("ukraine.png")},
                new Country { Id = 2,
                              Name = "Japan",
                              Description  = japanDesc,
                              ImageBase64 = ConvertImageToBase64("japan.png")},
                new Country { Id = 3,
                              Name = "USA",
                              Description  = usaDesc,
                              ImageBase64 = ConvertImageToBase64("usa.png")},
                new Country { Id = 4,
                              Name = "UK",
                              Description  = ukDesc,
                              ImageBase64 = ConvertImageToBase64("uk.png")},
                new Country { Id = 5,
                              Name = "Greece",
                              Description  = greeceDesc,
                              ImageBase64 = ConvertImageToBase64("greece.png")}
            };



            return countries;
        }

        private static string ConvertImageToBase64(string path)
        {
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            string imagePath = Path.Combine(wwwrootPath, path);
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(bytes);
            }
            catch (FileNotFoundException)
            {
                return string.Empty;
            }
        }
    }
}
