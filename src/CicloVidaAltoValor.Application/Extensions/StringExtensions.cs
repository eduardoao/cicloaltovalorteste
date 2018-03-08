using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CicloVidaAltoValor.Application.Enum;

namespace CicloVidaAltoValor.Application.Extensions
{
    public static class StringExtensions
    {
        public static HashSet<string> Ufs = new HashSet<string> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "SC", "SP", "SE", "TO", "RR" };

        private static readonly Lazy<Regex> BeAnEmailRegex = new Lazy<Regex>(() => new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$"));

        private static readonly Dictionary<char, char> equivalents = new Dictionary<char, char>();

        static StringExtensions()
        {
            const string origin = "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzªµºÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþ.-";
            const string destination = "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz   AAAAAA CEEEEIIII NOOOOO UUUUY  aaaaaa ceeeeiiii nooooo uuuuy .-";

            for (int i = 0; i < origin.Length; i++)
            {
                equivalents.Add(origin[i], destination[i]);
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool BeAnEmail(this string input)
        {
            return BeAnEmailRegex.Value.IsMatch(input.Trim());
        }

        public static string Clear(this string value)
        {
            var builder = new StringBuilder();

            foreach (char ch in value)
            {
                if (equivalents.ContainsKey(ch))
                    builder.Append(equivalents[ch]);
                else
                    builder.Append(' ');
            }

            return builder.ToString();
        }

        private static readonly Lazy<Regex> BeAPostalCodeRegex = new Lazy<Regex>(() => new Regex(@"^\d{5}((-)?\d{3})"));

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPostalCode(this string input)
        {
            return !string.IsNullOrEmpty(input) && BeAPostalCodeRegex.Value.IsMatch(input);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsInteger(this string arg)
        {
            return !string.IsNullOrWhiteSpace(arg) && arg.All(char.IsNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool IsState(this string state)
        {
            return Ufs.Contains(state);
        }

        public static bool IsCarteira(this string value)
        {
            Carteira carteira;
            return System.Enum.TryParse(value, true, out carteira);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsCpf(this string cpf)
        {

            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.RemoveFormatacao();


            if (cpf.Any(ch => !char.IsNumber(ch)))
            {
                return false;
            }

            if (cpf.Length != 11)
                return false;


            if (cpf == "00000000000" || cpf == "11111111111" ||
                cpf == "22222222222" || cpf == "33333333333" ||
                cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" ||
                cpf == "88888888888" || cpf == "99999999999")
                return false;

            var total = 0;
            var mod = 0;
            for (var i = 0; i < 9; i++)
            {
                var current = cpf[i];
                total += int.Parse(current.ToString()) * (i + 1);
            }
            mod = total % 11;

            if (mod == 10) mod = 0;

            if (mod.ToString() != cpf[9].ToString())
                return false;
            total = 0;
            for (var i = 0; i < 10; i++)
            {
                var current = cpf[i];
                total += int.Parse(current.ToString()) * (i);
            }
            mod = total % 11;

            if (mod == 10) mod = 0;

            return mod.ToString() == cpf[10].ToString();
        }


        public static string RemoveFormatacao(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = Regex.Replace(input, @"[A-Za-z./-]", "");

            return Regex.Replace(input, "\\s+", "");
        }

        private static Regex digitsOnly = new Regex(@"[^\d]");


        public static string JustNumbers(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return digitsOnly.Replace(value, "");
        }
        

        public static int IsNumber(this string value)
        {
            return string.IsNullOrEmpty(value) ? 0 : (value.All(char.IsNumber) ? Convert.ToInt32(value) : 0);
        }


        static string[] ones = new string[] { "", "Um", "Dois", "Tres", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
        static string[] teens = new string[] { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezeseis", "Dezesete", "Dezoito", "Dezenove" };
        static string[] tens = new string[] { "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
        static string[] thousandsGroups = { "", " Mil", " Milão", " Bilhão" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }

            string friendlyInt = leftDigits;

            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Cem"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
                if (n % 1000 == 0)
                {
                    return friendlyInt;
                }
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(this int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negativo " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }

        public static string RemoveVolts(this string value)
        {
            return value.Replace("- 110V", "").Replace("- 220V", "").Replace("110 V", "").Replace("220 V", "").Replace("110V", "").Replace("220V", "");
        }

    }
}
