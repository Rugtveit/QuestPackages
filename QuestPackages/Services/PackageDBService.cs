using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using QuestPackages.Models;

namespace QuestPackages.Services
{
    public class PackageDBService
    {
        private readonly IMongoCollection<DBPackage> _dbPackages;
        public PackageDBService(IPackageDatabaseSettings dbSettings) 
        {
            var mongoClient = new MongoClient(dbSettings.ConnectionString);
            var packageDB = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _dbPackages = packageDB.GetCollection<DBPackage>(dbSettings.DatabaseCollectionName);
        }

        public List<DBPackage> GetPackages() => _dbPackages.Find(dbPackage => true).ToList();

        public List<string> GetPackageIds()
        {
            var dbPackages = GetPackages();
            List<string> packageIds = new List<string>();
            foreach (var dbPackage in dbPackages) 
                packageIds.Add(dbPackage.Id);
            return packageIds;
        }

        public DBPackage GetPackage(string packageId) => _dbPackages.Find(dbPackage => dbPackage.Id == packageId).FirstOrDefault();

        public bool HasPackage(string packageId) 
        {
            if (_dbPackages.Find(dbPackage => dbPackage.Id == packageId)
                .CountDocuments() != 0) return true; 
            return false;
        }

        public DBPackage CreatePackage(DBPackage dBPackage) 
        {
            _dbPackages.InsertOne(dBPackage);
            return dBPackage;
        }

        public void UpdatePackage(string packageId, DBPackage packageInput) => _dbPackages.ReplaceOne(package => package.Id == packageId, packageInput);
    }
}
