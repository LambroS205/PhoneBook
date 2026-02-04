// See https://aka.ms/new-console-template for more information
using PhoneBook;

Console.WriteLine("SMART PHONEBOOK BY ....");
ContactRepository repo = new ContactRepository(200, 3);
Contact c1 = repo.CreateNewContact();
c1.SetName("Nguyen Van A");
c1.SetPhone("0937852464");
repo.Add(c1);
Contact c2 = repo.CreateNewContact();
c2.SetName("Tran Thi B");
c2.SetPhone("0937852464");
repo.Add(c2);
repo.PrintAllContacts();