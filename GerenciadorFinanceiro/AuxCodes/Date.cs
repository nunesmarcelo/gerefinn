using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorFinanceiro.AuxCodes
{
    public class Date
    {
        public static DateTime HoraBrasilia()
        {
            DateTime dateTime = DateTime.UtcNow;
            TimeZoneInfo horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, horaBrasilia);
        }
    }
}