// //Fireman->FiremanApi2->DataBaseInitializer.cs
// //andreygolubkow Андрей Голубков

using System.Collections.Generic;
using System.Linq;

using FiremanApi2.Model;

namespace FiremanApi2.DataBase
{
    public static class DataBaseInitializer
    {
        public static void Initialize(FireContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Operators.Any())
            {
                return;   // DB has been seeded
            }

            context.Operators.Add(new Operator()
                                  {
                                      Active = true,
                                      Fires = new List<Fire>(),
                                      GeoZone = new GpsPoint(),
                                      Id = 0,
                                      Key = "fireman",
                                      Login = "admin",
                                      Name = "admin",
                                      Role = "Administrator"
                                  });
            context.SaveChanges();
        }
    }
}
