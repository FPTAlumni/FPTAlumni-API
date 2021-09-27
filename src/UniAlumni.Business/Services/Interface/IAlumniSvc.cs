using System.Collections.Generic;
using UniAlumni.DataTier.Models;

namespace UniAlumni.Business.Services.Interface
{
    /// <summary>
    /// Interface for service layer of Alumni in Business module.
    /// </summary>
    public interface IAlumniSvc
    {
        /// <summary>
        /// Get list of all Alumni.
        /// </summary>
        /// <param name="requestQueryDto">An object contains manipulating and organizing criterias.</param>
        /// <param name="applyPagination">An criteria decide whether apply pagination or not.</param>
        /// <returns>List of Users.</returns>
        IEnumerable<Alumnus> GetAlumnis();

    }
}