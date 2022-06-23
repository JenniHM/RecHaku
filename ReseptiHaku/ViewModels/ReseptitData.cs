namespace ReseptiHaku.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Drawing;

    public class ReseptitData
    {
        public int ReseptiID { get; set; }
        public string ReseptinNimi { get; set; }
        public Nullable<int> AnnosKoko { get; set; }
        public Nullable<int> LoginID { get; set; }
        public Nullable<bool> Julkinen { get; set; }
        public int RiviID { get; set; }
        public int ReseptiAinesosaListaID { get; set; }
        public int RaakaAineID { get; set; }
        public decimal Maara { get; set; }
        public int MittayksikkoID { get; set; }
        public string RaakaAine { get; set; }
        public int KategoriaID { get; set; }
        public string Mittayksikko { get; set; }
        public string MittayksikkoSelite { get; set; }
        public string Kategoria { get; set; }
        public int ReseptiVaiheID { get; set; }
        public string ReseptiVaihe { get; set; }

    }
}