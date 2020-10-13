using System;
using System.Collections.Generic;
using System.Text;
using FlubuCore.Context.FluentInterface.Interfaces;

namespace FlubuCore.TeamsPlugin
{
    public static class TeamsExtensions
    {
        /// <summary>
        /// Microsoft teams specific tasks.
        /// </summary>
        /// <param name="flubu"></param>
        /// <returns></returns>
        public static Teams Teams(this ITaskFluentInterface flubu)
        {
            return new Teams();
        }
    }
}
