using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCustomerData
{
    /// <summary>
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public class Customer
    {
        #region Member variables
        public int CustomerNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Mail { get; private set; }
        public DateTime LastChange { get; private set; }
        public double BankBalance { get; private set; }

        private enum CallingClass
        {
            FirstName,
            LastName
        };

        #endregion

        #region Constructors
        public Customer(int customerNumber, string firstName, string lastName, string mail)
        {
            this.CustomerNumber = customerNumber;
            this.FirstName = this.CheckName(firstName, CallingClass.FirstName);
            this.LastName = this.CheckName(lastName, CallingClass.LastName);
            this.Mail = this.CheckMail(mail);
            this.LastChange = DateTime.Now;
            this.BankBalance = default(double);
        }
        public Customer(int customerNumber, string firstName, string lastName, string mail, double bankBalance)
        {
            this.CustomerNumber = customerNumber;
            this.FirstName = this.CheckName(firstName, CallingClass.FirstName);
            this.LastName = this.CheckName(lastName, CallingClass.LastName);
            this.Mail = this.CheckMail(mail);
            this.LastChange = DateTime.Now;
            this.BankBalance = bankBalance;
        }
        public Customer(int customerNumber, string firstName, string lastName, string mail, DateTime lastChange, double bankBalance)
        {
            this.CustomerNumber = customerNumber;
            this.FirstName = this.CheckName(firstName, CallingClass.FirstName);
            this.LastName = this.CheckName(lastName, CallingClass.LastName);
            this.Mail = this.CheckMail(mail);
            this.LastChange = lastChange;
            this.BankBalance = bankBalance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method changes the LastName of a customer. Keep in mind that the given name is checked and otherwise an exception is thrown.
        /// </summary>
        /// <param name="newName">New LastName of the customer</param>
        public void ChangeLastName(string newName)
        {
            // check string
            this.LastName = CheckName(newName, CallingClass.LastName);
            this.LastChange = DateTime.Now;
        }
        /// <summary>
        /// Methods changes the old Mail to the given new one if the one if it is correct. Otherwise an exception is thrown.
        /// </summary>
        /// <param name="newMail"></param>
        public void ChangeMail(string newMail)
        {
            // check mail if it is correct
            this.Mail = CheckMail(newMail);
            this.LastChange = DateTime.Now;
        }

        /// <summary>
        /// Methods changes the old bank balance to the given new one.
        /// </summary>
        /// <param name="newBankBalance"></param>
        public void ChangeBankBalance(double newBankBalance)
        {
            this.BankBalance = newBankBalance;
            this.LastChange = DateTime.Now;
        }

        /// <summary>
        /// Checks a mail adress considering different restrictions
        /// </summary>
        /// <param name="mail">Mail to check</param>
        /// <returns>Returns the given string if it is correct.</returns>
        private string CheckMail(string mail)
        {
            int cnt = 0;
            char[] symbols = new[] { '.', '!', '#', '$', '%', '&', '´', '`', '*', '+', '-', '/', '=', '?', '^', '_', '{', '}', '|', '@' };

            if (mail.Length >= 4 && mail.Contains('@'))
            {
                // check if only letters, digits and special symbols and at least one @ symbol
                foreach (char symb in mail)
                {
                    if (symb.Equals('@')) cnt++;
                    if (cnt >= 2) throw new InvalidDataException("ExceptionMailTooManyAt");
                    if (!char.IsLetterOrDigit(symb) && !symbols.Contains(symb)) throw new InvalidDataException("ExceptionMailForbSymb");
                }
                // check splitted strings
                string[] substrings = mail.Split('@');
                // check length of first substring
                if (substrings[0].Length < 1) throw new InvalidDataException("ExceptionMailFewSymbBeforeAt");
                // check if there is at least one symbol after @
                if (substrings[1].Length < 1) throw new InvalidDataException("ExceptionMailMuchSymbAfterAt");
                // check if there are points of forbidden positions
                if (substrings[0][0].Equals('.') || substrings[0][substrings[0].Length - 1].Equals('.') || substrings[1][0].Equals('.') || substrings[1][substrings[1].Length - 1].Equals('.'))
                    throw new InvalidDataException("ExpcetionMailFrobPoints");
                // check if there is only one . in the second sub
                cnt = 0;
                foreach (char symb in substrings[1])
                {
                    if (symb.Equals('.')) cnt++;
                    if (cnt >= 2) throw new InvalidDataException("ExpcetionMailToMuchPointsAfterAt");
                }
                if (cnt == 0) throw new InvalidDataException("ExceptionToLessPointsAfterAt");
                // check if there are 2-4 letters after the point
                string[] subSubStrings = substrings[1].Split('.');
                if (subSubStrings[1].Length < 2 || subSubStrings[1].Length > 4) throw new InvalidDataException("ExpcetionMailNumSymb");
                else
                {
                    foreach (char symb in subSubStrings[1])
                    {
                        if (!char.IsLetter(symb)) throw new InvalidDataException("ExceptionMailDomain");
                    }
                }
            }
            else
            {
                throw new InvalidDataException("ExceptionMailToShort");
            }
            return mail;
        }
        /// <summary>
        /// This methods tests a given String if it ensures the guidelines of a name. The method is able to distinguish between different calling classes.
        /// Otherwise an exception is thrown.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <param name="callingClass">Class which calls this method</param>
        /// <returns>Returns the checked string if correct</returns>
        private string CheckName(string name, CallingClass callingClass)
        {
            if (callingClass.Equals(CallingClass.FirstName))
            {
                if (name == null) throw new InvalidDataException("ExceptionFirstNameEmpty");
                else if (name.Length == 0) throw new InvalidDataException("ExceptionFirstNameEmpty");
                else if (char.IsLower(name[0])) throw new InvalidDataException("ExceptionFirstNameCapitalLetter");
                else
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        if(!Char.IsLetter(name[i])) throw new InvalidDataException("ExceptionFirstNameLetters");
                    }
                    return name;
                }
            }
            else if (callingClass.Equals(CallingClass.LastName))
            {
                if (name == null) throw new InvalidDataException("ExceptionLastNameEmpty");
                else if (name.Length == 0) throw new InvalidDataException("ExceptionLastNameEmpty");
                else if (char.IsLower(name[0])) throw new InvalidDataException("ExceptionLastNameCapitalLetter");
                else
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (!Char.IsLetter(name[i])) throw new InvalidDataException("ExceptionLastNameLetters");
                    }
                    return name;
                }
            }
            else return name;
        }

        #endregion


    }
}
