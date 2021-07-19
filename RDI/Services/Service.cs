using Microsoft.Extensions.Configuration;
using RDI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace RDI.Services
{
    public class Service : IService
    {
        static List<Service1Class> lstUsers = new List<Service1Class>();
        public Service1ClassResponse Service1(int IdCliente, long NrCard, int CVV)
        {
            try
            {

                Service1Class item = new Service1Class
                {
                    IdCliente = IdCliente,
                    NrCard = NrCard,
                    IDCard = lstUsers.Count + 1,
                    CVV = CVV

                };
                item.CriaToken();
                lstUsers.Add(item);

                Service1ClassResponse ret = new Service1ClassResponse
                {
                    DataRegsitro = item.DataRegsitro,
                    IDCard = item.IDCard,
                    Token = item.Token
                };
                return ret;
            }
            catch
            {
                return null;
            }

        }

        public bool Service2(int IdCliente, int IdCard, long Token, int CVV)
        {
            try
            {
                bool tempoValido = false;
                bool UserValido = false;
                foreach (Service1Class item in lstUsers)
                {
                    if (item.IDCard == IdCard)
                    {
                        if (item.DataRegsitro > DateTime.Now.AddMinutes(-30))
                        {
                            tempoValido = true;
                        }
                        if (IdCliente == item.IdCliente)
                        {
                            UserValido = true;
                        }

                    }
                    if (tempoValido && UserValido && item.ConfereToken(item.NrCard, Token))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public string teste()
        {
            return "API Online";
        }
    }

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Service1ClassResponse Service1(int IdCliente, long NrCard, int CVV);

        [OperationContract]
        bool Service2(int IdCliente, int IdCard, long Token, int CVV);

        [OperationContract]
        string teste();
    }
}
