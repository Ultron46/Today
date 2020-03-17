﻿using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Business.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects();
        Project GetProject(int id);
        bool InsertProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(int id);
    }
}
