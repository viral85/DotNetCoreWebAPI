using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using Newtonsoft.Json;

namespace MyTestProjectRepositoryLayer.BaseClasses
{
	public static class AutoMapper
	{
		public static List<T> ConvertDataTable<T>(DataTable dt)
		{
			try
			{
				List<T> ListOfClass = new List<T>();
				foreach (DataRow row in dt.Rows)
				{
					T itemobject = GetItem<T>(row);
					ListOfClass.Add(itemobject);
				}
				return ListOfClass;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
				throw;
			}
		}
		public static T GetItem<T>(DataRow dr)
		{
			string strColumnHavingProblem = string.Empty;
			try
			{
				Type classType = typeof(T);
				T classobject = Activator.CreateInstance<T>();
				foreach (DataColumn column in dr.Table.Columns)
				{
					foreach (PropertyInfo pro in classType.GetProperties())
					{
						strColumnHavingProblem = pro.Name;
						if (pro.Name.ToLower() == column.ColumnName.ToLower())
						{
							pro.SetValue(classobject, dr[column.ColumnName.ToLower()] != DBNull.Value ? dr[column.ColumnName.ToLower()] : (pro.PropertyType.IsValueType == true ? Activator.CreateInstance(pro.PropertyType) : String.Empty));
						}
						else
						{
							continue;
						}
					}
				}
				return classobject;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
				throw;
			}
		}
		public static T ConvertDataRow<T>(DataRow row)
		{
			try
			{
				T ListOfClass = GetItem<T>(row);
				return ListOfClass;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
				throw;
			}
		}

        public static string ConvertDataTableToJson(ref DataTable dataTable)
        {
            return JsonConvert.SerializeObject(dataTable);
        }
	}
}
