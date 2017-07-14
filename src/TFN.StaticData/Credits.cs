using System;
using System.Collections.Generic;

namespace TFN.StaticData
{
    public static class Credits
    {
        public static List<Domain.Models.Entities.Credits> Credit = new List<Domain.Models.Entities.Credits>
        {
            Domain.Models.Entities.Credits.Hydrate(new Guid("d796bb5f-bcf0-4430-adce-cf9c0bcb45f3"),
                new Guid("f42c8b85-c058-47cb-b504-57f750294469"), "bob","BOB", 100,new DateTime(2016,6,6,6,6,6),new DateTime(2016,6,6,6,6,6), true),
            Domain.Models.Entities.Credits.Hydrate(new Guid("d4c9d75b-c17b-4040-b647-6e4419ca670d"),
                new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"), "alice","ALICE", 5,new DateTime(2016,6,6,6,6,6),new DateTime(2016,6,6,6,6,6), true),
            Domain.Models.Entities.Credits.Hydrate(new Guid("8809eb82-2226-4e6e-a982-b2b9db0ce054"),
                new Guid("b984088b-bbab-4e3e-9a40-c07238475cb7"), "lutz","LUTZ", 50,new DateTime(2016,6,6,6,6,6),new DateTime(2016,6,6,6,6,6), true),
        };
    }
}