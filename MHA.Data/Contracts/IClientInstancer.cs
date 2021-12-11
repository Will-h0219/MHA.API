using MHA.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Contracts
{
    public interface IClientInstancer
    {
        IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : ModelBase;
    }
}
