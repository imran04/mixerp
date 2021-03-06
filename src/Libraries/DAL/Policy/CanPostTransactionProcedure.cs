// ReSharper disable All
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using MixERP.Net.Framework.Extensions;
using PetaPoco;
using MixERP.Net.Entities.Policy;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MixERP.Net.Schemas.Policy.Data
{
    /// <summary>
    /// Prepares, validates, and executes the function "policy.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date)" on the database.
    /// </summary>
    public class CanPostTransactionProcedure : DbAccess, ICanPostTransactionRepository
    {
        /// <summary>
        /// The schema of this PostgreSQL function.
        /// </summary>
        public override string _ObjectNamespace => "policy";
        /// <summary>
        /// The schema unqualified name of this PostgreSQL function.
        /// </summary>
        public override string _ObjectName => "can_post_transaction";
        /// <summary>
        /// Login id of application user accessing this PostgreSQL function.
        /// </summary>
        public long _LoginId { get; set; }
        /// <summary>
        /// User id of application user accessing this table.
        /// </summary>
        public int _UserId { get; set; }
        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string _Catalog { get; set; }

        /// <summary>
        /// Maps to "_login_id" argument of the function "policy.can_post_transaction".
        /// </summary>
        public long LoginId { get; set; }
        /// <summary>
        /// Maps to "_user_id" argument of the function "policy.can_post_transaction".
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Maps to "_office_id" argument of the function "policy.can_post_transaction".
        /// </summary>
        public int OfficeId { get; set; }
        /// <summary>
        /// Maps to "transaction_book" argument of the function "policy.can_post_transaction".
        /// </summary>
        public string TransactionBook { get; set; }
        /// <summary>
        /// Maps to "_value_date" argument of the function "policy.can_post_transaction".
        /// </summary>
        public DateTime ValueDate { get; set; }

        /// <summary>
        /// Prepares, validates, and executes the function "policy.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date)" on the database.
        /// </summary>
        public CanPostTransactionProcedure()
        {
        }

        /// <summary>
        /// Prepares, validates, and executes the function "policy.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date)" on the database.
        /// </summary>
        /// <param name="loginId">Enter argument value for "_login_id" parameter of the function "policy.can_post_transaction".</param>
        /// <param name="userId">Enter argument value for "_user_id" parameter of the function "policy.can_post_transaction".</param>
        /// <param name="officeId">Enter argument value for "_office_id" parameter of the function "policy.can_post_transaction".</param>
        /// <param name="transactionBook">Enter argument value for "transaction_book" parameter of the function "policy.can_post_transaction".</param>
        /// <param name="valueDate">Enter argument value for "_value_date" parameter of the function "policy.can_post_transaction".</param>
        public CanPostTransactionProcedure(long loginId, int userId, int officeId, string transactionBook, DateTime valueDate)
        {
            this.LoginId = loginId;
            this.UserId = userId;
            this.OfficeId = officeId;
            this.TransactionBook = transactionBook;
            this.ValueDate = valueDate;
        }
        /// <summary>
        /// Prepares and executes the function "policy.can_post_transaction".
        /// </summary>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public bool Execute()
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Execute, this._LoginId, this._Catalog, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the function \"CanPostTransactionProcedure\" was denied to the user with Login ID {LoginId}.", this._LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
            string query = "SELECT * FROM policy.can_post_transaction(@LoginId, @UserId, @OfficeId, @TransactionBook, @ValueDate);";

            query = query.ReplaceWholeWord("@LoginId", "@0::bigint");
            query = query.ReplaceWholeWord("@UserId", "@1::integer");
            query = query.ReplaceWholeWord("@OfficeId", "@2::integer");
            query = query.ReplaceWholeWord("@TransactionBook", "@3::text");
            query = query.ReplaceWholeWord("@ValueDate", "@4::date");


            List<object> parameters = new List<object>();
            parameters.Add(this.LoginId);
            parameters.Add(this.UserId);
            parameters.Add(this.OfficeId);
            parameters.Add(this.TransactionBook);
            parameters.Add(this.ValueDate);

            return Factory.Scalar<bool>(this._Catalog, query, parameters.ToArray());
        }


    }
}