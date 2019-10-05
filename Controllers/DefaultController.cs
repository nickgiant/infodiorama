﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infodiorama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace infodiorama.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
       // [HttpPost]  not
        //[ValidateAntiForgeryToken] not
        public ActionResult Index()
        {
            DbContext context = new DbContext();

            return View(context.GetRecords().ToList());
        }

        // GET: Default/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Entity/Edit/5

        public IActionResult Edit(int id)
        {
            /*if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {*/
                DbContext context = new DbContext();

                return View(context.GetEntityViewWithRecords(id));
            //}
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit()
        {

            DbContext context = new DbContext();

            return View(context.GetEntityViewWithRecords(1));
            //return NotFound();
        }*/

        // POST: Default/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]int id, ReadOnlyRecord[] rec) //[Bind("Id,Title,Adress,Description")]
        {


            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    DbContext context = new DbContext();
                    string queryfields = "";
                    object fieldValue = "";
                    for (int f = 0; f < rec.Length; f++)
                       {
                          // string val = HttpContext.Request.Form[rec[f].ColumnName];
                          fieldValue = rec[f].Value;

                            if (f == rec.Length - 1)
                           {
                               queryfields = queryfields + " " + rec[f].ColumnName + " = " + fieldValue;
                           }
                           else
                           {
                               queryfields = queryfields + " " + rec[f].ColumnName + " = " + fieldValue + ", ";
                           }
                           
                       }
                       context.Update(queryfields, id);
                       



                    /*
                    string queryfields = "";
                    for (int f = 0; f < entityView.FieldList.Count; f++)
                    {
                        object FieldValue = "";
                        for (var x = 0; x < entityView.Record[0].Count(); x++)
                        {

                            if (entityView.FieldList[f].Name.ToLower().Equals(entityView.Record[0].ElementAt(x).ColumnName.ToLower()))
                            {
                                string val = HttpContext.Request.Form[entityView.FieldList[f].Name];
                                FieldValue = entityView.Record[0].ElementAt(x).Value;
                                break;
                            }
                            else { FieldValue = "-"; }
                        }
                        if (f == entityView.FieldList.Count - 1)
                        {
                            queryfields = queryfields + " " + entityView.FieldList[f].Name + " = " + FieldValue;
                        }
                        else
                        {
                            queryfields = queryfields + " " + entityView.FieldList[f].Name + " = " + FieldValue + ", ";
                        }
                    }
                    context.Update(queryfields, Convert.ToInt32(entityView.Record[0].ElementAt(0).Value));
                    */

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }




        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}