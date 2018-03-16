using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibraryCustomerData
{
    public class CustomerDatabase
    {
        #region Member variables

        public int Count { get; private set; }
        private LinkedList<Customer> Customers;

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
        /// Returns 0 if successfull
        /// Returns -1 if email already exists
        /// </summary>
        /// <param name="firstName">First name of the new customer</param>
        /// <param name="lastName">Last name of the new customer</param>
        /// <param name="mail">Mail of the new customer</param>
        /// <param name="bankBalance">Bank balance of the new customer</param>
        /// <returns>Returns true if customer was added successfully</returns>
        public int AddCustomer(string firstName, string lastName, string mail, double bankBalance)
        {
            foreach (var cust in this.Customers)
            {
                if (cust.Mail == mail) return -2;
            }     

            this.Customers.AddLast(new Customer(this.Customers.Count, firstName, lastName, mail, bankBalance));
            this.Count = this.Customers.Count;

            return 0;
        }

        /// <summary>
        /// This method can be used to change the BankBalance of a given Customer.
        /// </summary>
        /// <param name="customerNumber">Customer number of the wanted customer</param>
        /// <param name="newBankBalance">The new bank balance for this customer</param>
        /// <returns>0 if ok / -1 if user was not found</returns>
        public int ChangeBankBalance(int customerNumber, double newBankBalance)
        {
            Customer currentCustomer = null;
            // search required customer in list
            foreach (var cust in this.Customers)
            {
                // set pointer if customer is found
                if (cust.CustomerNumber == customerNumber) currentCustomer = cust;
            }
            // a user was found
            if (currentCustomer != null)
            {
                currentCustomer.ChangeBankBalance(newBankBalance);
                return 0;
            }
            else return -1;
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
                if (cust.CustomerNumber == customerNumber) currentCustomer = cust;
                break;
            }
            // a user was found
            if (currentCustomer != null)
            {
                currentCustomer.ChangeLastName(newLastName);
            }
            else throw new InvalidDataException("User not found!");
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
                if (cust.CustomerNumber == customerNumber) currentCustomer = cust;
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
                    throw new InvalidDataException("This SortValue doesn't exist!");
                    break;
            }
            return sortedCustomers;
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
