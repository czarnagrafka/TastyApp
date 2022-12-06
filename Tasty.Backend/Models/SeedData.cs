using Tasty.Backend.Data;

namespace Tasty.Backend.Models
{
    public static class SeedData
    {
        public static void Initialize(AppDataContext context)
        {
            if (context.Restaurants.Any() || context.Cuisines.Any())
            {
                return;   // DB has been seeded
            }

            var polish = new Cuisine() { Name = "Polska" };
            var italian = new Cuisine() { Name = "Włoska" };
            var asian = new Cuisine() { Name = "Azjatycka" };
            var mexican = new Cuisine() { Name = "Meksykańska" };
            var burgers = new Cuisine() { Name = "Burgery" };
            var pizza = new Cuisine() { Name = "Pizza" };
            var coffee = new Cuisine() { Name = "Kawiarnia" };

            context.Cuisines.AddRange(
                polish, italian, asian,
                mexican, burgers, pizza, coffee);

            context.Restaurants.AddRange(
                new Restaurant()
                {
                    Name = "Zalipianki",
                    Cuisines = new[] { polish },
                    Latitude = 50.063000790747815,
                    Longitude = 19.933654471611256,
                    Radius = 500
                },
                new Restaurant()
                {
                    Name = "Marchewka z Groszkiem",
                    Cuisines = new[] { polish },
                    Latitude = 50.048527538327626,
                    Longitude = 19.945444973762335,
                    Radius = 400
                },
                new Restaurant()
                {
                    Name = "Cafe Lisboa",
                    Cuisines = new[] { coffee },
                    Latitude = 50.063856221711596,
                    Longitude = 19.928136019788365,
                    Radius = 350
                },
                new Restaurant()
                {
                    Name = "Magia Cafe",
                    Cuisines = new[] { coffee },
                    Latitude = 50.06224020314001,
                    Longitude = 19.933733011758314,
                    Radius = 200
                },
                new Restaurant()
                {
                    Name = "Urara Sushi & Shabu Shabu",
                    Cuisines = new[] { asian },
                    Latitude = 50.06303723861309,
                    Longitude = 19.93952888476952,
                    Radius = 150
                },
                new Restaurant()
                {
                    Name = "Manzana",
                    Cuisines = new[] { mexican, burgers },
                    Latitude = 50.052193278402896,
                    Longitude = 19.94614601769336,
                    Radius = 450
                },
                new Restaurant()
                {
                    Name = "Da Grasso",
                    Cuisines = new[] { italian, pizza },
                    Latitude = 50.05580660689492,
                    Longitude = 19.947017325173103,
                    Radius = 150
                },
                new Restaurant()
                {
                    Name = "Ramen People",
                    Cuisines = new[] { asian },
                    Latitude = 50.04647922627381,
                    Longitude = 19.95014221356886,
                    Radius = 350
                },
                new Restaurant()
                {
                    Name = "Bobby Burger",
                    Cuisines = new[] { burgers },
                    Latitude = 50.07016675312141,
                    Longitude = 19.944795699556092,
                    Radius = 320
                });

            context.SaveChanges();
        }
    }
}