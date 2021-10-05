﻿using Argon.Zine.Catalog.QueryStack.Models;
using MongoDB.Bson.Serialization;

namespace Argon.Zine.Catalog.Infra.Data.Queries.Mapping
{
    public static class BsonMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Restaurant>(rm =>
            {
                rm.AutoMap();
            });
        }
    }
}