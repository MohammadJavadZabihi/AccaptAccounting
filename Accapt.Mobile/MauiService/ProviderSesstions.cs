using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Mobile.MauiService
{
    public class ProviderSesstions
    {
        private static ProviderSesstions _instance;
        public static ProviderSesstions Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ProviderSesstions();
                }

                return _instance;
            }
        }

        public string Token { get; set; }
        public string PtoviderName { get; set; }
        public int ProviderId { get; set; }

        private ProviderSesstions() { }
    }
}
