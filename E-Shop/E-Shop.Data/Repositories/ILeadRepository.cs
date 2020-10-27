﻿using E_Shop.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public interface ILeadRepository
    {
        DataWrapper<LeadDTO> CreateLead(LeadDTO dto);
        DataWrapper<LeadDTO> DeleteLeadById(long Id);
    }
}
