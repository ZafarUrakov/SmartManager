﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {

        public StorageBroker()
        {
            this.Database.EnsureCreated();
        }

        public async ValueTask<T> InsertAsync<T>(T @object)
        {
                var broker = new StorageBroker();
                broker.Entry(@object).State = EntityState.Added;
                await broker.SaveChangesAsync();

                return @object;
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            var broker = new StorageBroker();

            return broker.Set<T>();
        }

        public async ValueTask<T> SelectAsync<T>(params object[] objectsId) where T : class
        {
            var broker = new StorageBroker();

            return await broker.FindAsync<T>(objectsId);
        }

        public async ValueTask<T> UpdateAsync<T>(T @object)
        {
            try
            {
                var broker = new StorageBroker();
                broker.Entry(@object).State = EntityState.Modified;
                await broker.SaveChangesAsync();

                return @object;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async ValueTask<T> DeleteAsync<T>(T @object)
        {
            try
            {
                var broker = new StorageBroker();
                broker.Entry(@object).State = EntityState.Deleted;
                await broker.SaveChangesAsync();

                return @object;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data source = Smart.db";
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlite(connectionString);
        }

        public override void Dispose() { }
    }
}
