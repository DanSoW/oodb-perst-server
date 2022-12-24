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

        /* ----- */
        /* Host Mock */
        public HostModel[] hosts = new[]
        {
            new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e2", "host 1", "http://host1.ru", "192.168.09.22", "ubuntu"),
            new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e3", "host 2", "http://host2.ru", "192.168.09.23", "windows"),
            new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e4", "host 3", "http://host3.ru", "192.168.09.22", "ubuntu"),
            new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e5", "host 4", "http://host4.ru", "192.168.11.22", "linux"),
            new HostModel("76baa1a3-2477-419c-9b9e-5f00eb5990e6", "host 5", "http://host5.ru", "192.168.09.22", "ubuntu"),
        };

        /* ----- */
        /* Admin Mock */
        public AdminModel[] admins = new[]
        {
            new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d131", "aaa@mail.ru"),
            new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d132", "awawh@mail.ru"),
            new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d133", "host@mail.ru"),
            new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d134", "abcdef@mail.ru"),
            new AdminModel("543732c6-bcb5-4504-af78-df2d87b4d135", "ubun@mail.ru"),
        };

        /* ----- */
        /* Data Sources Mock */
        public DataSourceModel[] dataSources = new[]
        {
            new DataSourceModel("2a864617-80aa-48c9-8010-bf24930abfe5", "app 1", "http://github.com/repository/1"),
            new DataSourceModel("2a864611-80aa-48c9-8010-bf24930abfe5", "app 2", "http://github.com/repository/2"),
            new DataSourceModel("2a864613-80aa-48c9-8010-bf24930abfe5", "app 3", "http://github.com/repository/3"),
            new DataSourceModel("2a864614-80aa-48c9-8010-bf24930abfe5", "app 4", "http://github.com/repository/4"),
            new DataSourceModel("2a864619-80aa-48c9-8010-bf24930abfe5", "app 5", "http://github.com/repository/5"),
        };

        /* ----- */
        /* Services Mock */
        public ServiceModel[] services = new[]
        {
            new ServiceModel
            {
                Id = "79036d6b-466c-4d59-93ba-d1e1c7a6392f",
                DataSourceId = "2a864617-80aa-48c9-8010-bf24930abfe5",
                Name = "Service 1",
                Port = 8080,
                TimeUpdate = 200,
            },
            new ServiceModel
            {
                Id = "fc2e7d45-355e-4d09-8a0d-efd084526a54",
                DataSourceId = "2a864611-80aa-48c9-8010-bf24930abfe5",
                Name = "Service 2",
                Port = 5000,
                TimeUpdate = 200,
            },
            new ServiceModel
            {
                Id = "b4b86e51-eece-4f0b-81b4-1443d2c63ad2",
                DataSourceId = "2a864619-80aa-48c9-8010-bf24930abfe5",
                Name = "Service 3",
                Port = 3000,
                TimeUpdate = 300,
            },
        };

        /* ----- */
        /* HostsServices Mock */
        public HostServiceModel[] hostsServices = new[]
        {
            new HostServiceModel
            {
                Id = "0f9597f9-f07e-4f4c-a91f-3be668d2db72",
                HostId = "76baa1a3-2477-419c-9b9e-5f00eb5990e3",
                ServiceId = "79036d6b-466c-4d59-93ba-d1e1c7a6392f"
            },
            new HostServiceModel
            {
                Id = "0f9593f5-f07e-4f4c-a91f-3be775d2db72",
                HostId = "76baa1a3-2477-419c-9b9e-5f00eb5990e6",
                ServiceId = "fc2e7d45-355e-4d09-8a0d-efd084526a54"
            },
            new HostServiceModel
            {
                Id = "0f9597f9-f07e-4f4c-a91f-3be775d2db72",
                HostId = "76baa1a3-2477-419c-9b9e-5f00eb5990e5",
                ServiceId = "2a864619-80aa-48c9-8010-bf24930abfe5"
            },
        };

        /* ----- */
        /* HostsServices Mock */
        public MonitorAppModel[] monitorApp = new[]
        {
            new MonitorAppModel
            {
                Id = "bb91dc52-87bb-4ded-ba90-89f2d5050a8f",
                HostId = "76baa1a3-2477-419c-9b9e-5f00eb5990e3",
                AdminId = "543732c6-bcb5-4504-af78-df2d87b4d135",
                Name = "Application 1",
                Url = "http://app1"
            },
            new MonitorAppModel
            {
                Id = "bb91dc52-87bb-4ded-ba90-89f2d5050a8j",
                HostId = "76baa1a3-2477-419c-9b9e-5f00eb5990e6",
                AdminId = "543732c6-bcb5-4504-af78-df2d87b4d134",
                Name = "Application 2",
                Url = "http://app2"
            },
            new MonitorAppModel
            {
                Id = "bb91dc52-87bb-4ded-ba90-89f2d5050a8e",
                HostId = "76baa1a3-2477-419c-9b9e-5f00eb5990e5",
                AdminId = "543732c6-bcb5-4504-af78-df2d87b4d133",
                Name = "Application 3",
                Url = "http://app3"
            },
        };

        /* ----- */
        public void generateData()
        {
            if ((_db == null) || (_root == null))
            {
                return;
            }

            foreach (var item in hosts)
            {
                _root.idxHost.Put(item);
            }

            foreach (var item in admins)
            {
                _root.idxAdmin.Put(item);
            }

            foreach (var item in dataSources)
            {
                _root.idxDataSource.Put(item);
            }

            foreach (var item in services)
            {
                _root.idxService.Put(item);
            }

            foreach (var item in hostsServices)
            {
                _root.idxHostService.Put(item);
            }

            foreach (var item in monitorApp)
            {
                _root.idxMonitorApp.Put(item);
            }
        }
    }
}
