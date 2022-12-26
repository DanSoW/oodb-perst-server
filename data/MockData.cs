using ConsoleApp1.root;
using oodb_project.models;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.data
{
    internal class MockData
    {
        private static Storage _db;
        private static PerstRoot _root;

        public MockData(Storage db, PerstRoot root)
        {
            _db = db;
            _root = root;
        }

        public DataSourceModel[] dataSources;
        public ServiceModel[] services;
        public HostModel[] hosts;
        public AdminModel[] admins;

        /* ----- */
        /* HostsServices Mock */
        public HostServiceModel[] hostsServices;

        /* ----- */
        /* HostsServices Mock */
        public MonitorAppModel[] monitorApp;

        public void generateMonitorApp()
        {
            monitorApp = new[]
            {
                new MonitorAppModel
                {
                    Id = "bb91dc52-87bb-4ded-ba90-89f2d5050a8f",
                    Host = null,
                    Admin = null,
                    Name = "Application 1",
                    Url = "http://app1"
                },
                new MonitorAppModel
                {
                    Id = "bb91dc52-87bb-4ded-ba90-89f2d5050a8j",
                    Host = null,
                    Admin = null,
                    Name = "Application 2",
                    Url = "http://app2"
                },
                new MonitorAppModel
                {
                    Id = "bb91dc52-87bb-4ded-ba90-89f2d5050a8e",
                    Host = null,
                    Admin = null,
                    Name = "Application 3",
                    Url = "http://app3"
                },
            };

            for(var i = 0; i < monitorApp.Length; i++)
            {
                monitorApp[i].Host = hosts[i];
                monitorApp[i].Admin = admins[i];

                hosts[i].MonitorAppLink.Add(monitorApp[i]);
                admins[i].MonitorAppLink.Add(monitorApp[i]);
            }

            foreach (var item in monitorApp)
            {
                _root.idxMonitorApp.Put(item);
            }
        }
        
        public void generateHosts()
        {
            hosts = new[]
            {
                new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e2", "host 1", 
                    "http://host1.ru", "192.168.09.22", "ubuntu", _db.CreateLink(), _db.CreateLink()),
                new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e3", "host 2", 
                    "http://host2.ru", "192.168.09.23", "windows", _db.CreateLink(), _db.CreateLink()),
                new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e4", "host 3", 
                    "http://host3.ru", "192.168.09.22", "ubuntu", _db.CreateLink(), _db.CreateLink()),
                new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e5", "host 4", 
                    "http://host4.ru", "192.168.11.22", "linux", _db.CreateLink(), _db.CreateLink()),
                new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e6", "host 5", 
                    "http://host5.ru", "192.168.09.22", "ubuntu", _db.CreateLink(), _db.CreateLink())
            };

            foreach (var item in hosts)
            {
                _root.idxHost.Put(item);
            }
        }

        public void generateAdmins()
        {
            admins = new[]
            {
                new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d131", "aaa@mail.ru", _db.CreateLink()),
                new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d132", "awawh@mail.ru", _db.CreateLink()),
                new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d133", "host@mail.ru", _db.CreateLink()),
                new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d134", "abcdef@mail.ru", _db.CreateLink()),
                new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d135", "ubun@mail.ru", _db.CreateLink()),
            };

            foreach (var item in admins)
            {
                _root.idxAdmin.Put(item);
            }
        }

        public void generateDataSource_Service()
        {
            dataSources = new[]
            {
                new DataSourceModel("2a864617-80aa-48c9-8010-bf24930abfe5", "app 1",
                    "http://github.com/repository/1", _db.CreateLink()),
                new DataSourceModel("2a864611-80aa-48c9-8010-bf24930abfe5", "app 2",
                    "http://github.com/repository/2", _db.CreateLink()),
                new DataSourceModel("2a864613-80aa-48c9-8010-bf24930abfe5", "app 3",
                    "http://github.com/repository/3", _db.CreateLink()),
                new DataSourceModel("2a864614-80aa-48c9-8010-bf24930abfe5", "app 4",
                    "http://github.com/repository/4", _db.CreateLink()),
                new DataSourceModel("2a864619-80aa-48c9-8010-bf24930abfe5", "app 5",
                    "http://github.com/repository/5", _db.CreateLink()),
            };

            foreach (var item in dataSources)
            {
                _root.idxDataSource.Put(item);
            }

            services = new[]
            {
                new ServiceModel
                {
                    Id = "79036d6b-466c-4d59-93ba-d1e1c7a6392f",
                    DataSource = null,
                    Name = "Service 1",
                    Port = 8080,
                    TimeUpdate = 200,
                    HostServiceLink = _db.CreateLink()
                },
                new ServiceModel
                {
                    Id = "fc2e7d45-355e-4d09-8a0d-efd084526a54",
                    DataSource = null,
                    Name = "Service 2",
                    Port = 5000,
                    TimeUpdate = 200,
                    HostServiceLink = _db.CreateLink()
                },
                new ServiceModel
                {
                    Id = "b4b86e51-eece-4f0b-81b4-1443d2c63ad2",
                    DataSource = null,
                    Name = "Service 3",
                    Port = 3000,
                    TimeUpdate = 300,
                    HostServiceLink = _db.CreateLink()
                },
            };

            // Добавление ссылок на экземпляры класса ServiceModel
            for (var i = 0; i < services.Length; i++)
            {
                dataSources[i].ServiceLink.Add(services[i]);
                services[i].DataSource = dataSources[i];
            }

            foreach (var item in services)
            {
                _root.idxService.Put(item);
            }
        }

        public void generateHostServices_Service()
        {
            hostsServices = new[]
            {
                new HostServiceModel
                {
                    Id = "0f9597f9-f07e-4f4c-a91f-3be668d2db72",
                    Host = null,
                    Service = null
                },
                new HostServiceModel
                {
                    Id = "0f9593f5-f07e-4f4c-a91f-3be775d2db72",
                    Host = null,
                    Service = null
                },
                new HostServiceModel
                {
                    Id = "0f9597f9-f07e-4f4c-a91f-3be775d2db72",
                    Host = null,
                    Service = null
                },
            };

            for (var i = 0; i < services.Length; i++)
            {
                services[i].HostServiceLink.Add(hostsServices[i]);
                hosts[i].HostServiceLink.Add(hostsServices[i]);
                hostsServices[i].Service = services[i];
                hostsServices[i].Host = hosts[i];
            }

            foreach (var item in hostsServices)
            {
                _root.idxHostService.Put(item);
            }
        }

        /* ----- */
        public void generateData()
        {
            if ((_db == null) || (_root == null))
            {
                return;
            }

            generateHosts();
            generateAdmins();
            generateDataSource_Service();
            generateHostServices_Service();
            generateMonitorApp();
        }
    }
}
