using GymManagement.DAL.Data.Dbcontext;
using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GymManagement.DAL.Data.DataSeeding
{
    public static class GymDataSeeding
    {

        public static async Task SeedAsync(GymDbcontext dbcontext, string seedingpath, ILogger logger, CancellationToken ct = default)
        {
            try {
                //seed data and messaee 
                //check there's data in plan  if no load from json file ad check json file if data  add and then check if there change andd save if no changes already adde and catch i
                if (!await dbcontext.plans.AnyAsync(ct))
                {
                    var plans = LoadDataFromJson<Plan>(seedingpath, "plan.json");
                    if (plans.Any())
                        dbcontext.AddRange(plans);
                    if (dbcontext.ChangeTracker.HasChanges()) { 
                        await dbcontext.SaveChangesAsync(ct);
                    logger.LogInformation($"Data seeded with count{plans.Count}"); }
                else logger.LogInformation("Already seeded");
                }
            }
        
            catch(Exception ex)
                {
                logger.LogError(ex, "cannot seed");
                throw;

            }


        }
        





    
    private static List<T> LoadDataFromJson<T>(string folderpath, string filename) {
            
            var filepath = Path.Combine(folderpath, filename);
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"Seed Data file not found{filepath}");
            }
            
             var data = File.ReadAllText(filepath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,

            };
            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();




        }
    }
}