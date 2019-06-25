using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heroes.Data.Models
{
    public interface IHeroDocumentStore: IDocumentStore<HeroDocument>
    {
    }
}
