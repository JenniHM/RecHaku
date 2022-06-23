namespace ReseptiHaku.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class MenutData
    {
        public int MenuID { get; set; }
        public string MenunNimi { get; set; }
        public int? LoginID { get; set; }
        public bool? Julkinen { get; set; }
        public int RiviID { get; set; }
        public int ReseptiID { get; set; }
        public int MenunKategoriaID { get; set; }
        public string MenunKategoria { get; set; }

    }
}