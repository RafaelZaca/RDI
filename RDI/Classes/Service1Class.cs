using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDI.Classes
{
    public class Service1Class
    {
        public int IdCliente { get; set; }
        public long NrCard { get; set; }
        public int CVV { get; set; }
        public DateTime DataRegsitro { get; private set; }
        public long Token { get; private set; }
        public int IDCard { get; set; }

        public void CriaToken()
        {
            if (NrCard < 1000)
            {
                throw new ArgumentOutOfRangeException("Número do cartão precisa de pelo menos 4 dígitos");
            }
            if (NrCard >= 10000000000000000)
            {
                throw new ArgumentOutOfRangeException("Número do cartão contém no máximo 16 dígitos");
            }
            if (CVV >= 100000)
            {
                throw new ArgumentOutOfRangeException("CVV contém no máximo 5 dígitos");
            }
            if (CVV < 0)
            {
                throw new ArgumentOutOfRangeException("CVV precisa ser maior que zero");
            }
            string subcard = NrCard.ToString().Substring(NrCard.ToString().Length - 4, 4);
            Token = Convert.ToInt64(subcard.Substring(CVV, subcard.Length + CVV) + subcard.Substring(0, -CVV));
            DataRegsitro = DateTime.Now;

        }
        public bool ConfereToken(long NrCardPass, long TokenPass)
        {
            string subcard = NrCardPass.ToString().Substring(NrCardPass.ToString().Length - 4, 4);
            return TokenPass == Convert.ToInt64(subcard.Substring(CVV, subcard.Length + CVV) + subcard.Substring(0, -CVV));


        }
    }
    public class Service1ClassResponse
    {
        public DateTime DataRegsitro { get; set; }
        public long Token { get; set; }
        public int IDCard { get; set; }
    }
}
