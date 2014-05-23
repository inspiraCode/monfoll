﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using SeguimientoSuper.Properties;

namespace SeguimientoSuper.Collectable.PostgresImpl
{
    public class Account : CommonBase
    {

        public DataTable Uncollectable()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "WHERE ctrl_cuenta.id_doco IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento=14);";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse( elapsed.TotalDays.ToString("0") );
            }


            return ds.Tables[0];
        }

        public void Uncollectable(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                    "VALUES(14, @documento, 'Documento marcado incobrable por gerencia.');";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Collectable(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_doco = @docId AND id_movimiento = 14;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(15, @documento, 'Cuenta Recuperada de incobrables.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void SetObservations(int docId, string collectType, string observations)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "UPDATE ctrl_cuenta " +
                "SET tipo_cobro = @collect_type, " +
                "observaciones = @observations " +
                "WHERE ID_DOCO = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@collect_type", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@observations", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@collect_type"].Value = collectType;
            cmd.Parameters["@observations"].Value = observations;
            cmd.Parameters["@id"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(16, @documento, 'Cuenta Modificada en AdminPaq por cobrador asignado.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void SetCollectDate(int docId, DateTime collectDate)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "UPDATE ctrl_cuenta " +
                "SET F_COBRO = @f_cobro " +
                "WHERE ID_DOCO = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@f_cobro", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@f_cobro"].Value = collectDate;
            cmd.Parameters["@id"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void ReOpen(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_doco = @docId AND id_movimiento = 9;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(11, @documento, 'Cuenta Abierta.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Unescale(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_doco = @docId AND id_movimiento = 4;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(13, @documento, 'Cuenta desescalada.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void UnReview(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_doco = @docId AND id_movimiento = 17;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(15, @documento, 'El supervisor quitó la bandera de revisión.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void UnAttend(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_doco = @docId AND id_movimiento = 16;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(15, @documento, 'El supervisor regresó la cuenta al cobrador.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Review(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_doco = @docId AND id_movimiento = 16;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(17, @documento, 'Cuenta revisada por supervisor.');";

            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Unassign(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_asignacion " +
                "WHERE id_doco = @docId;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(12, @documento, 'Cuenta deasignada.');";
            cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public DataTable Cancelled()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "WHERE ctrl_cuenta.id_doco IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento=10);";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable Closed()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "WHERE ctrl_cuenta.id_doco IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento=9);";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable Escalated()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "WHERE ctrl_cuenta.id_doco IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento=4);";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable Reviewed()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones, nombre_cobrador " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "INNER JOIN ctrl_asignacion ON ctrl_cuenta.id_doco = ctrl_asignacion.id_doco " +
                "INNER JOIN cat_cobrador ON ctrl_asignacion.id_cobrador = cat_cobrador.id_cobrador " +
                "WHERE ctrl_cuenta.id_doco IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento = 17);";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable Attended()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones, nombre_cobrador " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "INNER JOIN ctrl_asignacion ON ctrl_cuenta.id_doco = ctrl_asignacion.id_doco " +
                "INNER JOIN cat_cobrador ON ctrl_asignacion.id_cobrador = cat_cobrador.id_cobrador " +
                "WHERE ctrl_cuenta.id_doco IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento = 16);";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable Assigned()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones, nombre_cobrador " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "INNER JOIN ctrl_asignacion ON ctrl_cuenta.id_doco = ctrl_asignacion.id_doco " +
                "INNER JOIN cat_cobrador ON ctrl_asignacion.id_cobrador = cat_cobrador.id_cobrador " +
                "WHERE ctrl_cuenta.id_doco NOT IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento IN(4,9,10,16,17));";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable UnAssigned()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ctrl_cuenta.id_doco, ctrl_cuenta.ap_id, f_documento, f_vencimiento, f_cobro, ctrl_cuenta.id_cliente, cd_cliente, nombre_cliente, ruta, dia_pago, " +
                "CASE WHEN cat_cliente.es_local THEN 'Local' ELSE 'Foráneo' END AS area, " +
                "serie_doco, folio_doco, tipo_documento, tipo_cobro, facturado, saldo, moneda, observaciones " +
                "FROM ctrl_cuenta INNER JOIN cat_cliente ON ctrl_cuenta.id_cliente = cat_cliente.id_cliente " +
                "WHERE ctrl_cuenta.id_doco NOT IN(SELECT id_doco FROM ctrl_asignacion) " +
                "AND ctrl_cuenta.id_doco NOT IN(SELECT id_doco FROM ctrl_seguimiento WHERE id_movimiento IN(4,9,10,16,17));";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            ds.Tables[0].Columns.Add("dias_vencido", typeof(int));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime now = DateTime.Now;
                DateTime dueDate = DateTime.Parse(row["f_vencimiento"].ToString());
                TimeSpan elapsed = now.Subtract(dueDate);

                row["dias_vencido"] = int.Parse(elapsed.TotalDays.ToString("0"));
            }

            return ds.Tables[0];
        }

        public DataTable ReadSeries()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT DISTINCT serie_doco " +
                "FROM ctrl_cuenta;";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];
        }

        public DataTable ReadFolios()
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT folio_doco " +
                "FROM ctrl_cuenta;";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];
        }

        public DataTable FollowUp(int docId)
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;

            string sqlString = "SELECT id_seguimiento, ctrl_seguimiento.id_movimiento, cat_movimiento.descripcion AS movimiento, " +
                "system_based, ctrl_seguimiento.descripcion as seguimiento, ts_seguimiento " +
                "FROM ctrl_seguimiento INNER JOIN cat_movimiento ON ctrl_seguimiento.id_movimiento = cat_movimiento.id_movimiento " +
                "WHERE id_doco = " + docId.ToString() + ";";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();
            return ds.Tables[0];
        }

        public DataTable ReadPayments(int accountId)
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT ID_ABONO, TIPO_PAGO, IMPORTE_PAGO, FOLIO, CONCEPTO, FECHA_DEPOSITO, CUENTA " +
                "FROM CTRL_ABONO " +
                "WHERE ID_DOCO = " + accountId.ToString() + ";";

            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            conn.Close();
            return ds.Tables[0];
        }

        public void UploadAccounts(List<Collectable.Account> adminPaqAccounts, List<int> cancelled, List<int> saldados, Dictionary<int, Concepto> conceptos)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            Dictionary<int, Company> savedCompanies = new Dictionary<int, Company>();
            Dictionary<int, Concepto> savedConcepts = new Dictionary<int, Concepto>();
            foreach (Collectable.Account adminPaqAccount in adminPaqAccounts)
            {
                if (!savedCompanies.ContainsKey(adminPaqAccount.Company.ApId))
                {
                    SaveCompany(adminPaqAccount.Company);
                    savedCompanies.Add(adminPaqAccount.Company.ApId, adminPaqAccount.Company);
                }

                SaveAccount(adminPaqAccount);

                foreach (Collectable.Payment payment in adminPaqAccount.Payments)
                {
                    payment.DocId = GetDocIdFromAccount(adminPaqAccount);
                    SavePayment(payment);
                }

            }

            foreach (int AdminPaqId in cancelled)
            {
                int pgId = GetDocIdFromAdminPaq(AdminPaqId);
                if (pgId != -1)
                {
                    CancelAccount(pgId);
                }

            }

            foreach (int AdminPaqId in saldados)
            {
                int pgId = GetDocIdFromAdminPaq(AdminPaqId);
                if (pgId != -1)
                {
                    CloseBalancedAccount(pgId);
                }
            }

            foreach (Concepto concepto in conceptos.Values)
            {
                if (!savedConcepts.ContainsKey(concepto.APId))
                {
                    SaveConcepto(concepto);
                    savedConcepts.Add(concepto.APId, concepto);
                }
            }

            conn.Close();
        }

        private void CloseBalancedAccount(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                    "VALUES(9, @documento, 'Cuenta saldada en adminPaq');";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void CloseAccount(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                    "VALUES(9, @documento, 'Documento cerrado por supervisor');";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public void UpdateAccountById(Collectable.Account adminPaqAccount)
        {
            if (conn == null || conn.State != ConnectionState.Open) connect();

            string sqlString = "UPDATE ctrl_cuenta " +
                "SET F_DOCUMENTO = @f_documento, " +
                "F_VENCIMIENTO = @f_vencimiento, " +
                "F_COBRO = @f_cobro, " +
                "ID_CLIENTE = @id_cliente, " +
                "SERIE_DOCO = @serie_doco, " +
                "FOLIO_DOCO = @folio_doco, " +
                "TIPO_DOCUMENTO = @tipo_documento, " +
                "TIPO_COBRO = @tipo_cobro, " +
                "FACTURADO = @facturado, " +
                "SALDO = @saldo, " +
                "MONEDA = @moneda, " +
                "OBSERVACIONES = @observaciones, " +
                "TS_DESCARGADO = CURRENT_TIMESTAMP " +
                "WHERE ID_DOCO = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@f_documento", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@f_vencimiento", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@f_cobro", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@id_cliente", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@serie_doco", NpgsqlTypes.NpgsqlDbType.Varchar, 4);
            cmd.Parameters.Add("@folio_doco", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@tipo_documento", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@tipo_cobro", NpgsqlTypes.NpgsqlDbType.Varchar, 100);
            cmd.Parameters.Add("@facturado", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@saldo", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@moneda", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@observaciones", NpgsqlTypes.NpgsqlDbType.Varchar, 250);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@f_documento"].Value = adminPaqAccount.DocDate;
            cmd.Parameters["@f_vencimiento"].Value = adminPaqAccount.DueDate;
            cmd.Parameters["@f_cobro"].Value = adminPaqAccount.CollectDate;
            cmd.Parameters["@id_cliente"].Value = adminPaqAccount.Company.Id;
            cmd.Parameters["@serie_doco"].Value = adminPaqAccount.Serie;
            cmd.Parameters["@folio_doco"].Value = adminPaqAccount.Folio;
            cmd.Parameters["@tipo_documento"].Value = adminPaqAccount.DocType;
            cmd.Parameters["@tipo_cobro"].Value = adminPaqAccount.CollectType;
            cmd.Parameters["@facturado"].Value = adminPaqAccount.Amount;
            cmd.Parameters["@saldo"].Value = adminPaqAccount.Balance;
            cmd.Parameters["@moneda"].Value = adminPaqAccount.Currency;
            cmd.Parameters["@observaciones"].Value = adminPaqAccount.Note;
            cmd.Parameters["@id"].Value = adminPaqAccount.DocId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private int GetDocIdFromAccount(Collectable.Account account)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_doco " +
                "FROM ctrl_cuenta " +
                "WHERE ap_id = " + account.ApId.ToString() +
                " AND enterprise_id = " + account.Company.EnterpriseId.ToString() + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            return int.Parse(dt.Rows[0]["id_doco"].ToString());
        }

        private int GetDocIdFromAdminPaq ( int AdminPaqId)
        {
            Settings configusuario = Settings.Default;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_doco " +
                "FROM ctrl_cuenta " +
                "WHERE ap_id = " + AdminPaqId.ToString() +
                " AND enterprise_id = " + configusuario.empresa.ToString() + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return -1;
            }

            return int.Parse(dt.Rows[0]["id_doco"].ToString());
        }

        private void SaveConcepto(Concepto concepto)
        {
            if (!ConceptoExists(concepto))
            {
                AddConcepto(concepto);
            }
        }

        private void AddConcepto(Concepto concepto)
        {
            string sqlString = "INSERT INTO cat_concepto (ap_id, id_empresa, codigo_concepto, nombre_concepto, razon) " +
                "VALUES(@ap_id, @id_empresa, @codigo, @nombre, @razon);";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@ap_id", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@id_empresa", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@razon", NpgsqlTypes.NpgsqlDbType.Varchar, 50);

            cmd.Parameters["@ap_id"].Value = concepto.APId;
            cmd.Parameters["@id_empresa"].Value = concepto.IdEmpresa;
            cmd.Parameters["@codigo"].Value = concepto.Codigo;
            cmd.Parameters["@nombre"].Value = concepto.Nombre;
            cmd.Parameters["@razon"].Value = concepto.Razon;

            cmd.ExecuteNonQuery();
        }

        private bool ConceptoExists(Concepto concepto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_concepto " +
                "FROM cat_concepto " +
                "WHERE ap_id = " + concepto.APId.ToString() + " " +
                "AND id_empresa = " + concepto.IdEmpresa.ToString() + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            return dt.Rows.Count >= 1;
        }

        public void CancelAccount(int docId)
        {
            bool connected = false;
            if (conn == null || conn.State != ConnectionState.Open)
            {
                connect();
                connected = true;
            }
            
            if (IsCancelled(docId)) return;

            string sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(10, @docId, 'Documento cancelado en AdminPaq');";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);
            cmd.Parameters.Add("@docId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@docId"].Value = docId;

            cmd.ExecuteNonQuery();

            if (connected)
                conn.Close();
        }

        private bool IsCancelled(int docId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_doco FROM ctrl_seguimiento " +
                "WHERE id_doco = " + docId.ToString() + " " +
                "AND id_movimiento = 10;";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            return dt.Rows.Count >= 1;
        }

        private bool DocumentExists(int AdminPaqId, int EnterpriseId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_doco FROM ctrl_cuenta " +
                "WHERE ap_id = " + AdminPaqId.ToString() +
                " AND enterprise_id = " + EnterpriseId.ToString () + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            return dt.Rows.Count >= 1;
        }

        public void SavePayment(Payment payment)
        {
            bool connected = false;
            if (conn == null || conn.State != ConnectionState.Open)
            {
                connect();
                connected = true;
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_abono FROM ctrl_abono WHERE id_abono = " + payment.PaymentId.ToString() + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count >= 1)
                UpdatePayment(payment);
            else
                AddPayment(payment);

            if (connected) conn.Close();
        }

        private void UpdatePayment(Payment payment)
        {
            string sqlString = "UPDATE ctrl_abono " +
                "SET ID_DOCO = @id_doc, " +
                "TIPO_PAGO = @tipo_pago, " +
                "IMPORTE_PAGO = @importe, " +
                "FOLIO = @folio, " +
                "concepto = @concepto, " +
                "fecha_deposito = @fecha_deposito, " +
                "cuenta = @cuenta " +
                "WHERE ID_ABONO = @id";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@id_doc", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@tipo_pago", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@importe", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@folio", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@concepto", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@fecha_deposito", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@cuenta", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@id_doc"].Value = payment.DocId;
            cmd.Parameters["@tipo_pago"].Value = payment.PaymentType;
            cmd.Parameters["@importe"].Value = payment.Amount;
            cmd.Parameters["@folio"].Value = payment.Folio;
            cmd.Parameters["@concepto"].Value = payment.Concept;
            cmd.Parameters["@fecha_deposito"].Value = payment.DepositDate;
            cmd.Parameters["@cuenta"].Value = payment.Account;
            cmd.Parameters["@id"].Value = payment.PaymentId;

            cmd.ExecuteNonQuery();
        }

        private void AddPayment(Payment payment)
        {
            string sqlString = "INSERT INTO ctrl_abono (id_abono, id_doco, tipo_pago, importe_pago, folio, concepto, fecha_deposito, cuenta) " +
                "VALUES(@id, @id_doc, @tipo_pago, @importe, @folio, @concepto, @fecha_deposito, @cuenta);";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@id_doc", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@tipo_pago", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@importe", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@folio", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@concepto", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@fecha_deposito", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@cuenta", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@id_doc"].Value = payment.DocId;
            cmd.Parameters["@tipo_pago"].Value = payment.PaymentType;
            cmd.Parameters["@importe"].Value = payment.Amount;
            cmd.Parameters["@folio"].Value = payment.Folio;
            cmd.Parameters["@concepto"].Value = payment.Concept;
            cmd.Parameters["@fecha_deposito"].Value = payment.DepositDate;
            cmd.Parameters["@cuenta"].Value = payment.Account;
            cmd.Parameters["@id"].Value = payment.PaymentId;

            cmd.ExecuteNonQuery();
        }

        private void SaveCompany(Company adminPaqCompany)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_cliente " +
                "FROM cat_cliente " +
                "WHERE ap_id = " + adminPaqCompany.ApId.ToString() +
                " AND id_empresa = " + adminPaqCompany.EnterpriseId.ToString() + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count >= 1)
                UpdateCompany(adminPaqCompany);
            else
                AddCompany(adminPaqCompany);

        }

        private void UpdateCompany(Company adminPaqCompany)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "UPDATE cat_cliente " +
                "SET cd_cliente = @codigo, " +
                "nombre_cliente = @nombre_cliente, " +
                "ruta = @agente, " +
                "dia_pago = @dia_pago, " +
                "es_local = @local " +
                "WHERE ap_id = @id " +
                "AND id_empresa = @empresa";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Varchar, 6);
            cmd.Parameters.Add("@nombre_cliente", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@agente", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
            cmd.Parameters.Add("@dia_pago", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@local", NpgsqlTypes.NpgsqlDbType.Boolean);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@empresa", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@codigo"].Value = adminPaqCompany.Code;
            cmd.Parameters["@nombre_cliente"].Value = adminPaqCompany.Name;
            cmd.Parameters["@agente"].Value = adminPaqCompany.AgentCode;
            cmd.Parameters["@dia_pago"].Value = adminPaqCompany.PaymentDay;
            cmd.Parameters["@local"].Value = adminPaqCompany.EsLocal;
            cmd.Parameters["@id"].Value = adminPaqCompany.ApId;
            cmd.Parameters["@empresa"].Value = adminPaqCompany.EnterpriseId;

            cmd.ExecuteNonQuery();
        }

        private void AddCompany(Company adminPaqCompany)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO cat_cliente (ap_id, id_empresa, cd_cliente, nombre_cliente, ruta, dia_pago, es_local) " +
                "VALUES(@id, @empresa, @codigo, @nombre_cliente,  @agente,  @dia_pago, @local)";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@empresa", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Varchar, 6);
            cmd.Parameters.Add("@nombre_cliente", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@agente", NpgsqlTypes.NpgsqlDbType.Varchar, 20);
            cmd.Parameters.Add("@dia_pago", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@local", NpgsqlTypes.NpgsqlDbType.Boolean);

            cmd.Parameters["@id"].Value = adminPaqCompany.ApId;
            cmd.Parameters["@empresa"].Value = adminPaqCompany.EnterpriseId;
            cmd.Parameters["@codigo"].Value = adminPaqCompany.Code;
            cmd.Parameters["@nombre_cliente"].Value = adminPaqCompany.Name;
            cmd.Parameters["@agente"].Value = adminPaqCompany.AgentCode;
            cmd.Parameters["@dia_pago"].Value = adminPaqCompany.PaymentDay;
            cmd.Parameters["@local"].Value = adminPaqCompany.EsLocal;

            cmd.ExecuteNonQuery();
        }

        public void SaveAccount(Collectable.Account adminPaqAccount)
        {
            bool connected = false;
            if (conn == null || conn.State != ConnectionState.Open)
            {
                connect();
                connected = true;
            }

            if (DocumentExists(adminPaqAccount.ApId, adminPaqAccount.Company.EnterpriseId))
                UpdateAccount(adminPaqAccount);
            else
                AddAccount(adminPaqAccount);

            if (connected)
                conn.Close();
        }

        private void UpdateAccount(Collectable.Account adminPaqAccount)
        {
            string sqlString = "UPDATE ctrl_cuenta " +
                "SET F_DOCUMENTO = @f_documento, " +
                "F_VENCIMIENTO = @f_vencimiento, " +
                "F_COBRO = @f_cobro, " +
                "ID_CLIENTE = @id_cliente, " +
                "SERIE_DOCO = @serie_doco, " +
                "FOLIO_DOCO = @folio_doco, " +
                "TIPO_DOCUMENTO = @tipo_documento, " +
                "TIPO_COBRO = @tipo_cobro, " +
                "FACTURADO = @facturado, " +
                "SALDO = @saldo, " +
                "MONEDA = @moneda, " +
                "OBSERVACIONES = @observaciones, " +
                "TS_DESCARGADO = CURRENT_TIMESTAMP " +
                "WHERE ap_id = @id AND enterprise_id = @enterprise";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@f_documento", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@f_vencimiento", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@f_cobro", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@id_cliente", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@serie_doco", NpgsqlTypes.NpgsqlDbType.Varchar, 4);
            cmd.Parameters.Add("@folio_doco", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@tipo_documento", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@tipo_cobro", NpgsqlTypes.NpgsqlDbType.Varchar, 100);
            cmd.Parameters.Add("@facturado", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@saldo", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@moneda", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@observaciones", NpgsqlTypes.NpgsqlDbType.Varchar, 250);
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@enterprise", NpgsqlTypes.NpgsqlDbType.Integer);

            cmd.Parameters["@f_documento"].Value = adminPaqAccount.DocDate;
            cmd.Parameters["@f_vencimiento"].Value = adminPaqAccount.DueDate;
            cmd.Parameters["@f_cobro"].Value = adminPaqAccount.CollectDate;
            cmd.Parameters["@id_cliente"].Value = adminPaqAccount.Company.Id == 0 ? CompanyId(adminPaqAccount.Company.ApId, adminPaqAccount.Company.EnterpriseId) : adminPaqAccount.Company.Id;
            cmd.Parameters["@serie_doco"].Value = adminPaqAccount.Serie;
            cmd.Parameters["@folio_doco"].Value = adminPaqAccount.Folio;
            cmd.Parameters["@tipo_documento"].Value = adminPaqAccount.DocType;
            cmd.Parameters["@tipo_cobro"].Value = adminPaqAccount.CollectType;
            cmd.Parameters["@facturado"].Value = adminPaqAccount.Amount;
            cmd.Parameters["@saldo"].Value = adminPaqAccount.Balance;
            cmd.Parameters["@moneda"].Value = adminPaqAccount.Currency;
            cmd.Parameters["@observaciones"].Value = adminPaqAccount.Note;
            cmd.Parameters["@id"].Value = adminPaqAccount.ApId;
            cmd.Parameters["@enterprise"].Value = adminPaqAccount.Company.EnterpriseId;

            cmd.ExecuteNonQuery();
        }

        private void AddAccount(Collectable.Account adminPaqAccount)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_cuenta (ap_id, enterprise_id, F_DOCUMENTO, F_VENCIMIENTO, F_COBRO, ID_CLIENTE, SERIE_DOCO, FOLIO_DOCO, TIPO_DOCUMENTO, TIPO_COBRO, FACTURADO, " +
                "SALDO, MONEDA, OBSERVACIONES)" +
                "VALUES( @id, @enterprise, @f_documento, @f_vencimiento, @f_cobro, @id_cliente, @serie_doco, @folio_doco, @tipo_documento, @tipo_cobro, @facturado, @saldo, " +
                "@moneda, @observaciones);";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@enterprise", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@f_documento", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@f_vencimiento", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@f_cobro", NpgsqlTypes.NpgsqlDbType.Date);
            cmd.Parameters.Add("@id_cliente", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@serie_doco", NpgsqlTypes.NpgsqlDbType.Varchar, 4);
            cmd.Parameters.Add("@folio_doco", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@tipo_documento", NpgsqlTypes.NpgsqlDbType.Varchar, 150);
            cmd.Parameters.Add("@tipo_cobro", NpgsqlTypes.NpgsqlDbType.Varchar, 100);
            cmd.Parameters.Add("@facturado", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@saldo", NpgsqlTypes.NpgsqlDbType.Money);
            cmd.Parameters.Add("@moneda", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            cmd.Parameters.Add("@observaciones", NpgsqlTypes.NpgsqlDbType.Varchar, 250);

            cmd.Parameters["@id"].Value = adminPaqAccount.ApId;
            cmd.Parameters["@enterprise"].Value = adminPaqAccount.Company.EnterpriseId;
            cmd.Parameters["@f_documento"].Value = adminPaqAccount.DocDate;
            cmd.Parameters["@f_vencimiento"].Value = adminPaqAccount.DueDate;
            cmd.Parameters["@f_cobro"].Value = adminPaqAccount.CollectDate;
            cmd.Parameters["@id_cliente"].Value = CompanyId(adminPaqAccount.Company.ApId, adminPaqAccount.Company.EnterpriseId);
            cmd.Parameters["@serie_doco"].Value = adminPaqAccount.Serie;
            cmd.Parameters["@folio_doco"].Value = adminPaqAccount.Folio;
            cmd.Parameters["@tipo_documento"].Value = adminPaqAccount.DocType;
            cmd.Parameters["@tipo_cobro"].Value = adminPaqAccount.CollectType;
            cmd.Parameters["@facturado"].Value = adminPaqAccount.Amount;
            cmd.Parameters["@saldo"].Value = adminPaqAccount.Balance;
            cmd.Parameters["@moneda"].Value = adminPaqAccount.Currency;
            cmd.Parameters["@observaciones"].Value = adminPaqAccount.Note;

            cmd.ExecuteNonQuery();
        }

        private int CompanyId(int apId, int empresaId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da;
            string sqlString = "SELECT id_cliente " +
                "FROM cat_cliente " +
                "WHERE ap_id = " + apId.ToString() +
                " AND id_empresa = " + empresaId.ToString() + ";";
            da = new NpgsqlDataAdapter(sqlString, conn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            return int.Parse(dt.Rows[0]["id_cliente"].ToString());
        }

        public void AddFollowUp(string followUpType, string descripcion, int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_seguimiento (id_movimiento, id_doco, descripcion)" +
                "VALUES( @id_movimiento, @id_doco, @descripcion);";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@id_movimiento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@id_doco", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@descripcion", NpgsqlTypes.NpgsqlDbType.Varchar, 250);

            int id_movimiento = 8;
            switch (followUpType)
            {
                case "Llamada":
                    id_movimiento = 5;
                    break;
                case "Visita":
                    id_movimiento = 6;
                    break;
                case "Email":
                    id_movimiento = 7;
                    break;
                case "Cerrado":
                    id_movimiento = 9;
                    break;
                default:
                    id_movimiento = 8;
                    break;
            }

            cmd.Parameters["@id_movimiento"].Value = id_movimiento;
            cmd.Parameters["@id_doco"].Value = docId;
            cmd.Parameters["@descripcion"].Value = descripcion;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateFollowUp(string followUpType, string descripcion, int followUpId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "UPDATE ctrl_seguimiento " +
                "SET id_movimiento = @id_movimiento, " +
                "descripcion = @descripcion " +
                "WHERE id_seguimiento = @followUpId;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@id_movimiento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@descripcion", NpgsqlTypes.NpgsqlDbType.Varchar, 250);
            cmd.Parameters.Add("@followUpId", NpgsqlTypes.NpgsqlDbType.Integer);

            int id_movimiento = 8;
            switch (followUpType)
            {
                case "Llamada":
                    id_movimiento = 5;
                    break;
                case "Visita":
                    id_movimiento = 6;
                    break;
                case "Email":
                    id_movimiento = 7;
                    break;
                case "Cerrado":
                    id_movimiento = 9;
                    break;
                default:
                    id_movimiento = 8;
                    break;
            }

            cmd.Parameters["@id_movimiento"].Value = id_movimiento;
            cmd.Parameters["@descripcion"].Value = descripcion;
            cmd.Parameters["@followUpId"].Value = followUpId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void RemoveFollowUp(int followUpId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "DELETE FROM ctrl_seguimiento " +
                "WHERE id_seguimiento = @followUpId;";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@followUpId", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@followUpId"].Value = followUpId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Assign(int docId, int collectorId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_asignacion(id_cobrador, id_doco) " +
                "VALUES(@cobrador, @documento);";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@cobrador", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@cobrador"].Value = collectorId;
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Escale(int docId)
        {
            if (conn == null || conn.State != ConnectionState.Open)
                connect();

            string sqlString = "INSERT INTO ctrl_seguimiento(id_movimiento, id_doco, descripcion) " +
                "VALUES(4, @documento, 'Cuenta escalada a gerencia.');";

            NpgsqlCommand cmd = new NpgsqlCommand(sqlString, conn);

            cmd.Parameters.Add("@documento", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters["@documento"].Value = docId;

            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
