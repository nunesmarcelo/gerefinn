using GerenciadorFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GerenciadorFinanceiro.AuxCodes
{
    public class Sec
    {
        public static bool ExisteCPF(string CPF)
        {
            using (var db = new FinanceiroBanco())
            {
                var usuario = db.usuario.FirstOrDefault(u => u.cpf == CPF);

                return usuario != null;
            }
        }
        //public static bool ExisteCNPJCliente(string CNPJ)
        //{
        //    using (var db = new FinanceiroBanco())
        //    {
        //        var usuario = db.cliente.FirstOrDefault(u => u.cnpj == CNPJ);

        //        return usuario != null;
        //    }
        //}
        //public static bool ExisteCNPJFornecedor(string CNPJ)
        //{
        //    using (var db = new FinanceiroBanco())
        //    {
        //        var usuario = db.cliente.FirstOrDefault(u => u.cnpj == CNPJ);

        //        return usuario != null;
        //    }
        //}

        // chave de encriptação
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("cosme123");

        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes),
                CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string encryptedString)
        {
            if (String.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes),
                CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

        public static string SemAcento(string palavra)
        {
            string palavraSemAcento = null;
            string caracterComAcento = "áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇ";
            string caracterSemAcento = "aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC";

            for (int i = 0; i < palavra.Length; i++)
            {
                if (caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1))) >= 0)
                {
                    int car = caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1)));
                    palavraSemAcento += caracterSemAcento.Substring(car, 1);
                }
                else
                {
                    palavraSemAcento += palavra.Substring(i, 1);
                }
            }
            return palavraSemAcento;
        }
    }
}