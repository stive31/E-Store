﻿using Dapper;
using E_Shop.Core.Settings;
using E_Shop.Data.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace E_Shop.Data.Repositories
{
    public class LeadRepository : BaseRepository, ILeadRepository
    {
        public LeadRepository(IOptions<DBSettings> options)
        {
            DbConnection = new SqlConnection(options.Value.ConnectionString);
        }


        public DataWrapper<LeadDTO> CreateLead(LeadDTO dto)
        {
            var result = new DataWrapper<LeadDTO>();
            try
            {
                result.Data = DbConnection.Query<LeadDTO, RoleDTO, CityDTO, LeadDTO>(
                    StoredProcedure.CreateLeadProcedure,
                    (lead, role, city) =>
                    {
                        lead.Role = role;
                        lead.City = city;
                        return lead;
                    },
                    new
                    {
                        CityId = dto.City.Id,
                        dto.FirstName,
                        dto.LastName,
                        dto.Birthday,
                        dto.Address,
                        dto.Phone,
                        dto.Email,
                        dto.Password,
                    },
                     splitOn: "Id",
                    commandType: CommandType.StoredProcedure
                    ).SingleOrDefault();
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public DataWrapper<LeadDTO> UpdateLead(LeadDTO leadDto)
        {
            var data = new DataWrapper<LeadDTO>();
            try
            {
                data.Data = DbConnection.Query<LeadDTO, RoleDTO, CityDTO, LeadDTO>(
                    StoredProcedure.UpdateLeadByIdProcedure,
                    (lead, role, city) =>
                    {
                        lead.Role = role;
                        lead.City = city;
                        return lead;
                    },
                    new
                    {
                        leadDto.Id,
                        CityId = leadDto.City.Id,
                        leadDto.FirstName,
                        leadDto.LastName,
                        leadDto.RegistrationDate,
                        leadDto.Birthday,
                        leadDto.Address,
                        leadDto.Phone,
                        leadDto.Email,
                        RoleId = leadDto.Role.Id
                    },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure).
                    SingleOrDefault();
            }
            catch (Exception ex)
            {
                data.ErrorMessage = ex.Message;
            }
            return data;
        }

        public DataWrapper<LeadDTO> DeleteLeadById(long id)
        {
            var data = new DataWrapper<LeadDTO>();
            try
            {
                data.Data = DbConnection.Query<LeadDTO, RoleDTO, CityDTO, LeadDTO>(
                    StoredProcedure.DeleteLeadProcedure,
                    (lead, role, city) =>
                    {
                        lead.Role = role;
                        lead.City = city;
                        return lead;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure).
                    SingleOrDefault();
            }
            catch (Exception ex)
            {
                data.ErrorMessage = ex.Message;
            }
            return data;
        }
    }
}
