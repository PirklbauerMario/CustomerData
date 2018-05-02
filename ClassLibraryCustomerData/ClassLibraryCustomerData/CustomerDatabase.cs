using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibraryCustomerData
{
    public class CustomerDatabase
    {
        #region Member variables

        /// <summary>
        /// Gives the amount of customers inside the Database.
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Gives a list of all customers inside the Database.
        /// </summary>
        public LinkedList<Customer> Customers { get; private set; }

        #endregion

        #region Constructors

        public CustomerDatabase()
        {
            this.Customers = new LinkedList<Customer>();
            this.Count = this.Customers.Count;
        }

        public CustomerDatabase(string firstName, string lastName, string mail, double bankBalance)
        {
            this.Customers = new LinkedList<Customer>();
            this.Customers.AddLast(new Customer(this.Customers.Count, firstName, lastName, mail, bankBalance));
            this.Count = this.Customers.Count;
        }

        public CustomerDatabase(Customer newCustomer)
        {
            this.Customers = new LinkedList<Customer>();
            this.Customers.AddLast(newCustomer);
            this.Count = this.Customers.Count;
        }

        public CustomerDatabase(LinkedList<Customer> customers)
        {
            this.Customers = customers;
            this.Count = this.Customers.Count;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method is used to add a customer to the database. Alle parameters are required.
        /// If name of email is incorrect an exception is raised.
        /// Returns true if successfull.
        /// An exception is raised if email still exists.
        /// </summary>
        /// <param name="firstName">First name of the new customer</param>
        /// <param name="lastName">Last name of the new customer</param>
        /// <param name="mail">Mail of the new customer</param>
        /// <param name="bankBalance">Bank balance of the new customer</param>
        /// <returns>Returns true if customer was added successfully</returns>
        public bool AddCustomer(string firstName, string lastName, string mail, double bankBalance)
        {
            foreach (var cust in this.Customers)
            {
                if (cust.Mail == mail)
                    throw new InvalidDataException("ExceptionMailExists");
            }

            this.Customers.AddLast(new Customer(this.Customers.Count, firstName, lastName, mail, bankBalance));
            this.Count = this.Customers.Count;

            return true;
        }

        /// <summary>
        /// This method can be used to change the BankBalance of a given Customer.
        /// If the customer doesn't exist an exception is raised.
        /// </summary>
        /// <param name="customerNumber">Customer number of the wanted customer</param>
        /// <param name="newBankBalance">The new bank balance for this customer</param>
        /// <returns>True if Changing was successful</returns>
        public bool ChangeBankBalance(int customerNumber, double newBankBalance)
        {
            Customer currentCustomer = null;
            // search required customer in list
            foreach (var cust in this.Customers)
            {
                // set pointer if customer is found
                if (cust.CustomerNumber == customerNumber)
                {
                    currentCustomer = cust;
                    break;
                }
            }

            // a user was found
            if (currentCustomer != null)
            {
                currentCustomer.ChangeBankBalance(newBankBalance);
                return true;
            }
            else throw new InvalidDataException("ExceptionCustNumForb");
        }

        /// <summary>
        /// This method can be used to change the last name of a given Customer. If user does not exist, an exception is thrown.
        /// </summary>
        /// <param name="customerNumber">Customer number of the wanted customer</param>
        /// <param name="newLastName">The new last name for this customer</param>
        public void ChangeLastName(int customerNumber, string newLastName)
        {
            Customer currentCustomer = null;
            // search required customer in list
            foreach (var cust in this.Customers)
            {
                // set pointer if customer is found
                if (cust.CustomerNumber == customerNumber)
                {
                    currentCustomer = cust;
                    break;
                }
            }

            // a user was found
            if (currentCustomer != null)
            {
                currentCustomer.ChangeLastName(newLastName);
            }
            else throw new InvalidDataException("ExceptionUserNotFound");
        }

        /// <summary>
        /// This method can be used to change the mail of a given Customer.
        /// </summary>
        /// <param name="customerNumber">Customer number of the wanted customer</param>
        /// <param name="newMail">The new mail for this customer</param>
        /// <returns>0 if ok / -1 if user was not found / exception if mail was incorrect</returns>
        public int ChangeMail(int customerNumber, string newMail)
        {
            Customer currentCustomer = null;
            // search required customer in list
            foreach (var cust in this.Customers)
            {
                // set pointer if customer is found
                if (cust.CustomerNumber == customerNumber)
                {
                    currentCustomer = cust;
                    break;
                }
            }

            // a user was found
            if (currentCustomer != null)
            {
                currentCustomer.ChangeMail(newMail);
                return 0;
            }
            else return -1;
        }

        /// <summary>
        /// This method sorts the customer database regarding the given sortValue.
        /// </summary>
        /// <param name="sortValue">0->customernumber / 1->firstName / 2->lastname / 3->mail / 4->lastChange / 5->bankBalance </param>
        /// <returns>A list with sorted customers</returns>
        public LinkedList<Customer> GetSortedCustomers(int sortValue)
        {
            int cnt = 0;
            LinkedList<Customer> sortedCustomers = new LinkedList<Customer>();
            const int customernumber = 0;
            const int firstName = 1;
            const int lastName = 2;
            const int mail = 3;
            const int lastChange = 4;
            const int bankBalance = 5;

            switch (sortValue)
            {
                case customernumber:
                    cnt = 0;
                    // Create Array of customernumbers
                    int[] customerNumbers = new int[this.Customers.Count];
                    foreach (var customer in this.Customers)
                    {
                        customerNumbers[cnt] = customer.CustomerNumber;
                        cnt++;
                    }

                    // Sort Array
                    QuickSort<int>(customerNumbers, 0, customerNumbers.Length - 1);
                    // Create new list with sorted array
                    cnt = 0;
                    while (sortedCustomers.Count != this.Customers.Count)
                    {
                        foreach (var customer in this.Customers)
                        {
                            // customer found and is still not in sortedlist --> add to sorted list --> inc cnt
                            if ((customer.CustomerNumber == customerNumbers[cnt]) &&
                                (sortedCustomers.Contains(customer) == false))
                            {
                                sortedCustomers.AddLast(customer);
                                cnt++;
                            }
                            else continue;
                        }
                    }

                    break;
                case firstName:
                    cnt = 0;
                    // Create Array of customernumbers
                    string[] customerFirstNames = new string[this.Customers.Count];
                    foreach (var customer in this.Customers)
                    {
                        customerFirstNames[cnt] = customer.FirstName;
                        cnt++;
                    }

                    // Sort Array
                    QuickSort<string>(customerFirstNames, 0, customerFirstNames.Length - 1);
                    // Create new list with sorted array
                    cnt = 0;
                    while (sortedCustomers.Count != this.Customers.Count)
                    {
                        foreach (var customer in this.Customers)
                        {
                            // customer found and is still not in sortedlist --> add to sorted list --> inc cnt
                            if ((customer.FirstName == customerFirstNames[cnt]) &&
                                (sortedCustomers.Contains(customer) == false))
                            {
                                sortedCustomers.AddLast(customer);
                                cnt++;
                                break;
                            }
                            else continue;
                        }
                    }

                    break;
                case lastName:
                    cnt = 0;
                    // Create Array of customernumbers
                    string[] customerLastNames = new string[this.Customers.Count];
                    foreach (var customer in this.Customers)
                    {
                        customerLastNames[cnt] = customer.LastName;
                        cnt++;
                    }

                    // Sort Array
                    QuickSort<string>(customerLastNames, 0, customerLastNames.Length - 1);
                    // Create new list with sorted array
                    cnt = 0;
                    while (sortedCustomers.Count != this.Customers.Count)
                    {
                        foreach (var customer in this.Customers)
                        {
                            // customer found and is still not in sortedlist --> add to sorted list --> inc cnt
                            if ((customer.LastName == customerLastNames[cnt]) &&
                                (sortedCustomers.Contains(customer) == false))
                            {
                                sortedCustomers.AddLast(customer);
                                cnt++;
                                break;
                            }
                            else continue;
                        }
                    }

                    break;
                case mail:
                    cnt = 0;
                    // Create Array of customernumbers
                    string[] customerMails = new string[this.Customers.Count];
                    foreach (var customer in this.Customers)
                    {
                        customerMails[cnt] = customer.Mail;
                        cnt++;
                    }

                    // Sort Array
                    QuickSort<string>(customerMails, 0, customerMails.Length - 1);
                    // Create new list with sorted array
                    cnt = 0;
                    while (sortedCustomers.Count != this.Customers.Count)
                    {
                        foreach (var customer in this.Customers)
                        {
                            // customer found and is still not in sortedlist --> add to sorted list --> inc cnt
                            if ((customer.Mail == customerMails[cnt]) &&
                                (sortedCustomers.Contains(customer) == false))
                            {
                                sortedCustomers.AddLast(customer);
                                cnt++;
                                break;
                            }
                            else continue;
                        }
                    }

                    break;
                case lastChange:
                    cnt = 0;
                    // Create Array of customernumbers
                    DateTime[] customerDateChanged = new DateTime[this.Customers.Count];
                    foreach (var customer in this.Customers)
                    {
                        customerDateChanged[cnt] = customer.LastChange;
                        cnt++;
                    }

                    // Sort Array
                    QuickSort<DateTime>(customerDateChanged, 0, customerDateChanged.Length - 1);
                    // Create new list with sorted array
                    cnt = 0;
                    while (sortedCustomers.Count != this.Customers.Count)
                    {
                        foreach (var customer in this.Customers)
                        {
                            // customer found and is still not in sortedlist --> add to sorted list --> inc cnt
                            if ((customer.LastChange == customerDateChanged[cnt]) &&
                                (sortedCustomers.Contains(customer) == false))
                            {
                                sortedCustomers.AddLast(customer);
                                cnt++;
                                break;
                            }
                            else continue;
                        }
                    }

                    break;
                case bankBalance:
                    cnt = 0;
                    // Create Array of customernumbers
                    Double[] customerBankBalances = new Double[this.Customers.Count];
                    foreach (var customer in this.Customers)
                    {
                        customerBankBalances[cnt] = customer.BankBalance;
                        cnt++;
                    }

                    // Sort Array
                    QuickSort<Double>(customerBankBalances, 0, customerBankBalances.Length - 1);
                    // Create new list with sorted array
                    cnt = 0;
                    while (sortedCustomers.Count != this.Customers.Count)
                    {
                        foreach (var customer in this.Customers)
                        {
                            // customer found and is still not in sortedlist --> add to sorted list --> inc cnt
                            if ((customer.BankBalance == customerBankBalances[cnt]) &&
                                (sortedCustomers.Contains(customer) == false))
                            {
                                sortedCustomers.AddLast(customer);
                                cnt++;
                                break;
                            }
                            else continue;
                        }
                    }

                    break;
                default:
                    throw new InvalidDataException("ExceptionQuickSortForb");
                    break;
            }

            return sortedCustomers;
        }

        /// <summary>
        /// This method is used to store a Customer Database in a CSV-File.
        /// The file is stored in the your projects bin/debug directory.
        /// You don't have to give an ending for your filename (e.g. .csv) because this is added by this method.
        /// For the reason that the file should not be human-readable an encryption takes place depending on the given key.
        /// The key should have at least 1 and max. 16 charaters. Otherwise an exception is raised.
        /// </summary>
        /// <param name="filename">Filename </param>
        /// <param name="password"></param>
        /// <returns>Returns true if saving was successul</returns>
        public bool SaveDatabaseToCSVEncrypted(string filename, string password)
        {
            string path = filename + ".csv";
            string line;
            string ciphertext;
            // check length of Filename
            if (filename.Length <= 0) throw new InvalidDataException("ExceptionInvalFileName");

            // check if password has at least 16 characters
            if (password.Length > 16 || password.Length <= 0) throw new InvalidDataException("ExceptionWrongPassw");

            StreamWriter strWriter = File.CreateText(path);

            // Write each customer to file
            foreach (var cust in this.Customers)
            {
                line = cust.CustomerNumber.ToString() + ';' + cust.FirstName + ';' + cust.LastName + ';' + cust.Mail + ';' +
                       cust.LastChange.Year + ';' + cust.LastChange.Month + ';' + cust.LastChange.Day + ';' +
                       cust.LastChange.Hour + ';' + cust.LastChange.Minute + ';' + cust.LastChange.Second + ';' +
                       cust.BankBalance.ToString();

                // Encrypt String
                ciphertext = Encode(line, CalcKeyFromPassword(password));
                // Write encrypted string to csv
                strWriter.WriteLine(ciphertext);
            }

            // Close StreamWriter
            strWriter.Close();
            return true;
        }

        /// <summary>
        /// This method is used to load a Customer Database from a CSV-File.
        /// The file should be stored in the your projects bin/debug directory.
        /// You don't have to give an ending for your filename (e.g. .csv) because this is added by this method.
        /// The file has to be descrypted with the given key.
        /// An exception is raised if the file doesn't exist.
        /// The key should have at least 1 and max. 16 charaters. Otherwise an exception is raised.
        /// ATTENTION: Your database should be empty when loading from a file. Otherwise an exception is raised. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <returns>True if loading was successful</returns>
        public bool LoadDatabaseFromCsvDecrypted(string filename, string password)
        {
            string path = filename + ".csv";
            string line;
            string plaintext;
            string[] lineSplitted;
            int customerNumber, year, month, day, hour, minute, second;
            string firstName, lastName, mail;
            DateTime lastChange;
            double bankBalance;

            // check if password has at least 16 characters
            if (password.Length > 16 || password.Length <= 0) throw new InvalidDataException("ExceptionWrongPassw");
            // Create instance of streamreader
            try
            {
                StreamReader strReader = File.OpenText(path);

                if (this.Customers.Count == 0)
                {
                    while (strReader.Peek() != -1)
                    {
                        line = strReader.ReadLine();
                        // decrypt
                        plaintext = Decode(line, CalcKeyFromPassword(password));
                        // split at ;
                        lineSplitted = plaintext.Split(';');

                        // check if there were too many split elements ;
                        if (lineSplitted.Length != 11) throw new InvalidDataException("ExceptionInvalPassw");
                        else
                        {
                            if (!int.TryParse(lineSplitted[0], out customerNumber)) throw new InvalidDataException("ExceptionCustNumForb");
                            firstName = lineSplitted[1];
                            lastName = lineSplitted[2];
                            mail = lineSplitted[3];
                            if (!int.TryParse(lineSplitted[4], out year)) throw new InvalidDataException("ExceptionYearForb");
                            if (!int.TryParse(lineSplitted[5], out month)) throw new InvalidDataException("ExceptionMonthForb");
                            if (!int.TryParse(lineSplitted[6], out day)) throw new InvalidDataException("ExceptionDayForb");
                            if (!int.TryParse(lineSplitted[7], out hour)) throw new InvalidDataException("ExceptionHourForb");
                            if (!int.TryParse(lineSplitted[8], out minute)) throw new InvalidDataException("ExceptionMinuteForb");
                            if (!int.TryParse(lineSplitted[9], out second)) throw new InvalidDataException("ExceptionSecondForb");
                            lastChange = new DateTime(year, month, day, hour, minute, second);
                            if (!double.TryParse(lineSplitted[10], out bankBalance)) throw new InvalidDataException("ExceptionBankBalForb");

                            this.Customers.AddLast(new Customer(customerNumber, firstName, lastName, mail, lastChange, bankBalance));
                        }
                    }
                }
                else throw new InvalidDataException("ExceptionDBNotEmpty");

                // Close StreamReader
                strReader.Close();
                // Refresh Count
                this.Count = this.Customers.Count;

                return true;
            }
            catch (FileNotFoundException e)
            {
                throw new InvalidDataException("ExceptionInvalFileName");
            }
        }

        /// <summary>
        /// This method can be used to export the whole database to a Binaryfile. 
        /// The file is stored in the your projects bin/debug directory.
        /// You don't have to give an ending for your filename (e.g. .dat) because this is added by this method.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>True if saveing was successful</returns>
        private bool SaveDatabaseToBinaryFile(string filename)
        {
            string fileName = filename + ".dat";

            // Create BinaryWriter and write to file
            BinaryWriter binWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));

            // Write each customer to file
            foreach (var cust in this.Customers)
            {
                binWriter.Write(cust.CustomerNumber);
                binWriter.Write(cust.FirstName);
                binWriter.Write(cust.LastName);
                binWriter.Write(cust.Mail);
                binWriter.Write(cust.LastChange.Year);
                binWriter.Write(cust.LastChange.Month);
                binWriter.Write(cust.LastChange.Day);
                binWriter.Write(cust.LastChange.Hour);
                binWriter.Write(cust.LastChange.Minute);
                binWriter.Write(cust.LastChange.Second);
                binWriter.Write(cust.BankBalance);
            }
            // Close binarayWriter after execution
            binWriter.Close();
            return true;
        }

        /// <summary>
        /// This method can be used to load customers from a presafed file.
        /// You don't have to give an ending for your filename (e.g. .dat) because this is added by this method.
        /// An exception is raised if the file doesn't exist.
        /// ATTENTION: Your database should be empty when loading from a file. Otherwise an exception is raised. 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>True if loading was successful</returns>
        private bool LoadDatabaseFromBinaryFile(string filename)
        {
            string fileName = filename + ".dat";
            int customerNumber;
            string firstName, lastName, mail;
            DateTime lastChange;
            double bankBalance;

            // Create BinaryReader
            BinaryReader binReader = new BinaryReader(File.Open(fileName, FileMode.Open));

            if (this.Customers.Count == 0)
            {
                while (binReader.PeekChar() != -1)
                {
                    customerNumber = binReader.ReadInt32();
                    firstName = binReader.ReadString();
                    lastName = binReader.ReadString();
                    mail = binReader.ReadString();
                    lastChange = new DateTime(binReader.ReadInt32(), binReader.ReadInt32(), binReader.ReadInt32(),
                        binReader.ReadInt32(), binReader.ReadInt32(), binReader.ReadInt32());
                    bankBalance = binReader.ReadDouble();

                    this.Customers.AddLast(new Customer(customerNumber, firstName, lastName, mail, lastChange, bankBalance));
                }
            }
            else throw new InvalidDataException("ExceptionDBNotEmpty");
            // Close binReader whenn successful
            binReader.Close();
            // Refresh Count
            this.Count = this.Customers.Count;

            return true;
        }

        /// <summary>
        /// This method can be used to calculate a Key for Encrypting/Decrypting from a given Password. The ASCII-values from each symbol of the string is summed up and finally the reminder of mod 56 is used as key.
        /// </summary>
        /// <param name="password">Given password</param>
        /// <returns>Key for encryption/decryption</returns>
        private int CalcKeyFromPassword(string password)
        {
            int key = 0;

            for (int i = 0; i < password.Length; i++)
            {
                key += (int) password[i];
            }
            key = key % 57;
            return key;
        }

        /// <summary>
        /// This method is used to encrypt a string with Cäsar-Encrypting depending on the given key.
        /// </summary>
        /// <param name="plaintext">String which should be encrypted</param>
        /// <param name="key">Key used to encrypt</param>
        /// <returns></returns>
        private string Encode(string plaintext, int key)
        {
            {
                string ausgabe = "";

                for (int i = 0; i < plaintext.Length; i++)
                {
                    char c = plaintext[i];
                    ausgabe += (char)(c + key);
                }
                return ausgabe;
            }
        }

        /// <summary>
        /// This method is used to decrypt a string with Cäsar-Decrypting depending on the given key.
        /// </summary>
        /// <param name="ciphertext">String which should be decrypted</param>
        /// <param name="key">Key used to decrypt</param>
        /// <returns></returns>
        private string Decode(string ciphertext, int key)
        {
            string ausgabe = "";

            for (int i = 0; i < ciphertext.Length; i++)
            {
                char c = ciphertext[i];
                ausgabe += (char)(c - key);
            }
            return ausgabe;
        }

        private byte[] EncodeAES(string plaintext, string key, string IV)
        {
            Rijndael AESCrypto = Rijndael.Create();
            AESCrypto.Key = System.Text.Encoding.UTF8.GetBytes(key);
            AESCrypto.IV = System.Text.Encoding.UTF8.GetBytes(IV);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, AESCrypto.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] PlainBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
            cs.Write(PlainBytes, 0, PlainBytes.Length);
            cs.Close();

            byte[] EncryptedBytes = ms.ToArray();
            return EncryptedBytes;
        }
        private string DecodeAES(byte[] encryptedBytes, string key, string IV)
        {
            Rijndael AESCrypto = Rijndael.Create();
            AESCrypto.Key = System.Text.Encoding.UTF8.GetBytes(key);
            AESCrypto.IV = System.Text.Encoding.UTF8.GetBytes(IV);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, AESCrypto.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(encryptedBytes, 0, encryptedBytes.Length);
            cs.Close();

            byte[] DecryptedBytes = ms.ToArray();
            return System.Text.Encoding.UTF8.GetString(DecryptedBytes);
        }

        /// <summary>
        /// This method uses QuickSort to sort a given Array.
        /// Because of the fact that the array is ref, the elements array is sorted afterwards.
        /// </summary>
        /// <typeparam name="T">The datatype of the array</typeparam>
        /// <param name="elements">The given array to sort</param>
        /// <param name="left">First element in Array</param>
        /// <param name="right">Last element in Array</param>
        private static void QuickSort<T>(T[] elements, int left, int right) where T : IComparable<T>
        {
            int i = left, j = right;
            T pivot = elements[(left + right) / 2];
            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    T tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                QuickSort(elements, left, j);
            }

            if (i < right)
            {
                QuickSort(elements, i, right);
            }
        }

        #endregion

    }
}
