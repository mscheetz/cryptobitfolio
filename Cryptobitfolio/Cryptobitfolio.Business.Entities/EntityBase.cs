// -----------------------------------------------------------------------------
// <copyright file="EntityBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:32:49 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Entities
{
    #region Usings

    using SQLite;

    #endregion Usings

    public class EntityBase
    {
        #region Properties

        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }

        #endregion Properties
    }
}