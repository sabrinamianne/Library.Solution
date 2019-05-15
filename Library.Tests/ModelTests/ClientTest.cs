using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.TestTools
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_CLient()
    {
      string name = "David Jones";
      Client newClient = new Client(name);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_ClientName()
    {
      Client clientName = new Client("Sabrina Kherrab");
      string otherName = "Sabrina Kherrab";
      Assert.AreEqual(otherName, clientName.GetName());
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameAreTheSame_CLient()
    {
      Client firstClient = new Client ("Sabrina Mianne");
      Client secondClient = new Client ("Sabrina Mianne");
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void GetId_ReturnId_Id()
    {
      Client newClient = new Client ("Sabrina Mianne", 1);
      int idClient = 1;
      Assert.AreEqual(idClient, newClient.GetId());
    }

    [TestMethod]
    public void GetAllCLients_ReturnsAllClients()
    {
      Client firstClient = new Client ("Sabrina Mianne", 1);
      Client secondClient = new Client ("Sandra Kherrab", 1);
      List<Client> newList = new List<Client> {firstClient, secondClient};
      List<Client> listClients = Client.GetAll();
      CollectionAssert.AreEqual(newList, newList);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      Client testClient = new Client ("Sonia Kherrab");
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientsList()
    {
      string name1 = "Jimmy Mian";
      string name2 = "Eric Mian";
      Client newClient1 = new Client(name1);
      newClient1.Save();
      Client newClient2 = new Client(name2);
      newClient2.Save();
      List<Client> newList = new List<Client> {newClient1, newClient2};

      List <Client> result = Client.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Client testClient = new Client("Jessica Martinez");
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {

      Client testClient = new Client ("Laure Gonthier");
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());

      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      string firstClient = "Sylvia Mano";
      Client testClient = new Client(firstClient);
      testClient.Save();
      string secondClient= "Lauria Dana";

      testClient.Edit(secondClient);
      string result = Client.Find(testClient.GetId()).GetName();
    }


  }
}
