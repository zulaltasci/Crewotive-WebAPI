using BTI_Project1_API.Attributes;
using BTI_Project1_API.Context;
using BTI_Project1_API.Controllers;
using BTI_Project1_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BTI_Project1_API.Helper
{
    public static class Convert
    {
        public static async Task<_Person> DbToPersonAsync(Person person, ApplicationDbContext context)
        {
            _Person convertedPerson = new _Person();

            #region Same property Content Cloning

            #region Unneeded

            //convertedPerson.Id = person.Id;
            //convertedPerson.Name = person.Name;
            //convertedPerson.Surname = person.Surname;
            //convertedPerson.Role = person.Role;
            //convertedPerson.GithubLink = person.GithubLink;
            //convertedPerson.LinkedInLink = person.LinkedInLink;

            //foreach (var property in typeof(Person).GetProperties())
            //{
            //    if (property.Name.ToString() == "ProjectIds") continue;
            //    var editedProp = property.GetValue(person);
            //    property.SetValue(convertedPerson, editedProp);
            //}

            #endregion

            convertedPerson = Helper.Copy.Action(person, convertedPerson);

            #endregion

            #region Ids to Project Arrays

            List<int> ids = new List<int>();

            try
            {
                foreach (var id in person.ProjectIds.Split('-'))
                {
                    try
                    {
                        ids.Add(int.Parse(id));
                    }
                    catch (Exception) { }
                }
            } 
            catch (ArgumentNullException) { }

            List<Project> projects = new List<Project>();

            foreach (var id in ids)
            {
                projects.Add(await context.Project.FindAsync(id));
            }

            convertedPerson.Projects = projects.ToArray();

            #endregion

            return convertedPerson;
        }

        public static async Task<ActionResult<IEnumerable<_Person>>> DbToPersonListAsync(ApplicationDbContext context, bool IsAll = false)
        {
            List<_Person> _People = new List<_Person>();

            foreach (var item in context.Person.ToList())
            {
                if (!IsAll && !item.IsActive) continue;

                _People.Add(await DbToPersonAsync(item, context));
            }

            return _People;
        }

        public static async Task<_Project> DbToProjectAsync(Project project, ApplicationDbContext context)
        {
            _Project convertedProject = new _Project();

            #region Same property Content Cloning

            #region Unneeded

            //convertedProject.Id = project.Id;
            //convertedProject.Name = project.Name;
            //convertedProject.Explanation = project.Explanation;
            //convertedProject.GithubLink = project.GithubLink;

            //foreach (var property in typeof(Project).GetProperties())
            //{
            //    if (property.GetCustomAttributes(typeof(IgnoreCopy), false).Length > 0) continue;

            //    var property1 = typeof(_Project).GetProperty(property.Name);
            //    property1.SetValue(convertedProject, property.GetValue(project));
            //}

            #endregion

            convertedProject = Helper.Copy.Action(project, convertedProject);

            #endregion

            #region Ids to Project Arrays

            List<int> ids = new List<int>();

            try
            {
                foreach (var id in project.PersonIds.Split('-'))
                {
                    try
                    {
                        ids.Add(int.Parse(id));
                    }
                    catch (Exception) { }
                }
            }catch(Exception) { }

            List<Person> People = new List<Person>();

            foreach (var id in ids)
            {
                Person tempPerson = await context.Person.FindAsync(id);

                tempPerson.Password = "******";

                People.Add(tempPerson);
            }

            convertedProject.People = People.ToArray();

            #endregion

            return convertedProject;
        }

        public static async Task<ActionResult<IEnumerable<_Project>>> DbToProjectListAsync(ApplicationDbContext context, bool IsAll = false)
        {
            List<_Project> _Projects = new List<_Project>();

            foreach (var item in context.Project.ToList())
            {
                if (!IsAll && !item.IsActive) continue;

                _Projects.Add(await DbToProjectAsync(item, context));
            }

            return _Projects;
        }

        public static Person PersonToDb(Person person, ApplicationDbContext context)
        {
            //Person convertedPerson = new Person();

            person.IsActive = true;

            #region Finding Person Id

            string tempId;
            try
            {
                tempId = (context.Person.Select(i => i.Id).Max() + 1).ToString();
            }
            catch (Exception)
            {
                tempId = "1";
            }

            #endregion

            #region UNNEEDED
            #region Project Array to Ids

            //List<string> ids = new List<string>();

            //foreach (var project in person.Projects)
            //{
            //    ids.Add(project.Id.ToString());
            //}

            //string ProjectIds = ids.Count == 1 ? ids[0] : String.Join('-', ids);

            #endregion
            #endregion

            #region Adding Person Id in Projects

            try
            {
                foreach (var project in context.Project)
                {
                    if (person.ProjectIds.Contains(project.Id.ToString()))
                    {
                        List<string> personIds = project.PersonIds.Length == 0 ? new List<string>() : project.PersonIds.Split('-').ToList();
                        personIds.Add(tempId);
                        personIds.Sort();
                        project.PersonIds = personIds.Count == 1 ? personIds[0] : String.Join('-', personIds);
                    }
                }
            }catch(Exception) { }

            #endregion

            return person;
        }

        public static Project ProjectToDb(Project project, ApplicationDbContext context)
        {
            //Project convertedProject = new Project();

            project.IsActive = true;

            #region Finding Project Id

            string tempId;
            try
            {
                tempId = (context.Project.Select(i => i.Id).Max() + 1).ToString();
            }
            catch(Exception)
            {
                tempId = "1";
            }
            #endregion

            #region UNNEEDED
            #region Project Arrays to Ids

            //List<string> ids = new List<string>();

            //foreach (var person in project.People)
            //{
            //    ids.Add(person.Id.ToString());
            //}

            //string PersonIds = String.Join('-', ids);

            #endregion
            #endregion

            #region Adding Project Id in People

            try
            {
                foreach (var person in context.Person)
                {
                    if (project.PersonIds.Contains(person.Id.ToString()))
                    {
                        List<string> projectIds = person.ProjectIds.Length == 0 ? new List<string>() : person.ProjectIds.Split('-').ToList();
                        projectIds.Add(tempId);
                        projectIds.Sort();
                        person.ProjectIds = projectIds.Count == 1 ? projectIds[0] : String.Join('-', projectIds);
                    }
                }
            }catch(Exception) { }

            #endregion

            return project;
        }
    }
}