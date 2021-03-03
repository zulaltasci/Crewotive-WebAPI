using BTI_Project1_API.Attributes;
using BTI_Project1_API.Context;
using BTI_Project1_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Helper
{
    public static class Convert
    {
        public static async Task<_Person> DbToPersonAsync(Person person, ApplicationDbContext context)
        {
            _Person convertedPerson = new _Person();

            #region Same property Content Cloning

            convertedPerson.Id = person.Id;
            convertedPerson.Name = person.Name;
            convertedPerson.Surname = person.Surname;
            convertedPerson.Role = person.Role;
            convertedPerson.GithubLink = person.GithubLink;
            convertedPerson.LinkedInLink = person.LinkedInLink;

            //foreach (var property in typeof(Person).GetProperties())
            //{
            //    if (property.Name.ToString() == "ProjectIds") continue;
            //    var editedProp = property.GetValue(person);
            //    property.SetValue(convertedPerson, editedProp);
            //}
            #endregion

            #region Ids to Project Arrays
            List<int> ids = new List<int>();

            foreach (var id in person.ProjectIds.Split('-')) 
            {
                ids.Add(int.Parse(id));
            }

            List<Project> projects = new List<Project>();

            foreach (var id in ids)
            {
                projects.Add(await context.Project.FindAsync(id));
            }

            convertedPerson.Projects = projects.ToArray();
            #endregion

            return convertedPerson;
        }

        public static async Task<ActionResult<IEnumerable<_Person>>> DbToPersonListAsync(ApplicationDbContext context)
        {
            List<_Person> _People = new List<_Person>();

            foreach (var item in context.Person)
            {
                _People.Add(await DbToPersonAsync(item, context));
            }

            return _People;
        }

        public static async Task<_Project> DbToProjectAsync(Project project, ApplicationDbContext context)
        {
            _Project convertedProject = new _Project();

            #region Same property Content Cloning
            convertedProject.Id = project.Id;
            convertedProject.Name = project.Name;
            convertedProject.Explanation = project.Explanation;
            convertedProject.GithubLink = project.GithubLink;

            //foreach (var property in typeof(Project).GetProperties())
            //{
            //    //if (property.GetCustomAttributes(typeof(IgnoreCopy), false).Length > 0) continue;
            //    property.SetValue(project, property.GetValue(convertedProject));
            //}
            #endregion

            #region Ids to Project Arrays
            List<int> ids = new List<int>();

            foreach (var id in project.PersonIds.Split('-'))
            {
                ids.Add(int.Parse(id));
            }

            List<Person> People = new List<Person>();

            foreach (var id in ids)
            {
                People.Add(await context.Person.FindAsync(id));
            }

            convertedProject.People = People.ToArray();
            #endregion

            return convertedProject;
        }

        public static async Task<ActionResult<IEnumerable<_Project>>> DbToProjectListAsync(ApplicationDbContext context)
        {
            List<_Project> _Projects = new List<_Project>();

            foreach (var item in context.Project)
            {
                _Projects.Add(await DbToProjectAsync(item, context));
            }

            return _Projects;
        }
    
        
        public static async Task<Person> PersonToDbAsync(_Person _person)
        {
            Person person = new Person();

            return person;
        }

        public static async Task<Person> PersonToDbAsync(_Person person, ApplicationDbContext context)
        {
            return new Person();
        }

        public static async Task<Project> ProjectToDbAsync(_Project project, ApplicationDbContext context)
        {
            return new Project();
        }
    }
}