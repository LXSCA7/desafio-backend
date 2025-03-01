using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Api.Context;
using Desafio.Api.Controllers;

namespace Desafio.Api.Models
{
    public static class Document
    {
        public static string FormatCPF(string CPF)
        {
            StringBuilder newCPF = new();
            for (int i = 0; i < CPF.Length; i++)
            {
                newCPF.Append(CPF[i]);
                if (i == 2 || i == 5)
                    newCPF.Append('.');
                if (i == 8)
                    newCPF.Append('-');
            }

            return newCPF.ToString();
        }

        public static string RemoveDocumentDigits(string document)
        {
            StringBuilder newDocument = new();
            foreach (char c in document)
                if (char.IsDigit(c))
                    newDocument.Append(c);

            return newDocument.ToString();
        }
        public static bool ValidCPF(string CPF)
        {
            if (CPF.Length != 11)
                return false;

            int[] newCPF = new int[11];
            for (int i = 0; i < CPF.Length; i++)
            {
                newCPF[i] = int.Parse(CPF[i].ToString());  
            }

            int firstVerification = 0;
            for (int i = 0; i < 9; i++)
            {
                firstVerification += newCPF[i] * (10 - i);
            }

            firstVerification = firstVerification * 10 % 11;
            if (firstVerification == 10 || firstVerification == 11)
                firstVerification = 0;
            if (firstVerification != newCPF[9])
                return false;
            
            int secondVerification = 0;
            for (int i = 0; i < 10; i++)
            {
                secondVerification += newCPF[i] * (11 - i);
            }

            secondVerification = secondVerification * 10 % 11;
            if (secondVerification == 10 || secondVerification == 11)
                secondVerification = 0;
            if (secondVerification != newCPF[10])
                return false;

            return true;
        }  
    }
}