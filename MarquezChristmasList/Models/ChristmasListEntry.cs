using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace MarquezChristmasList.Models
{
    public class ChristmasListEntry
    {
        [Name("Name")]
        public string Name { get; set; }
        [Name("Family")]
        public string Family { get; set; }

        [Name("2018")]
        public string GiftTo2018 { protected get; set; }
        [Name("2019")]
        public string GiftTo2019 { protected get; set; }
        [Name("2020")]
        public string GiftTo2020 { protected get; set; }
        [Name("2021")]
        public string GiftTo2021 { protected get; set; }
        [Name("2022")]
        public string GiftTo2022 { protected get; set; }
        [Name("2023")]
        public string GiftTo2023 { protected get; set; }

        public IList<string> GiftTo => new List<string> { GiftTo2023, GiftTo2022, GiftTo2021, GiftTo2020, GiftTo2019, GiftTo2018  };
    }
}
