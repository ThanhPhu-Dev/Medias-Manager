using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomModels
{
    public class InfoPaymentHistoryCustomModelcs
    {
        public int STT { get; set; }
        public string DateOfPayment { get; set; }
        public string Note { get; set; }
        public long Price { get; set; }

        public InfoPaymentHistoryCustomModelcs()
        {

        }
        public InfoPaymentHistoryCustomModelcs(Payment_History pm)
        {
            DateOfPayment = pm.DateOfPayment.Value.ToString("d");
            Note = pm.Note;
            Price = long.Parse(pm.Price.Value.ToString());
        }
    }
}
