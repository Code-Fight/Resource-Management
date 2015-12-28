using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KASXBean;
using System.Configuration;
using DAL;
using DAL.Data;
using KASXBean.System;

namespace KASXDataBase.Data
{
    /// <summary>
    /// 供地图查询的sql
    /// </summary>
    public class DBMapSql
    {
        protected IDataBase _database;
        private string _strdatabasename;
        public DBMapSql(string strdatabasename)
        {
            _strdatabasename = strdatabasename;
            _database = new DBBase(_strdatabasename);
        }
 
        public DataSet ExecuteDataSet(string strsql)
        {
            try
            {
                return _database.ExecuteDataSet(strsql);
            }
            catch (Exception e)
            {
                Log.WriteError(e.Message);
                throw e;
            }
        }

        public void ExecuteNonQuery(string strsql)
        {
            try
            {
                _database.ExecuteNonQuery(strsql);
            }
            catch(Exception e)
            {
                Log.WriteError(strsql); 
                Log.WriteError(e.Message);
                throw e;
            }
        }
        public IDataReader ExecuteReader(string strsql)
        {
            try
            {
                return _database.ExecuteReader(strsql);
            }
            catch (Exception e)
            {
                Log.WriteError(strsql);
                Log.WriteError(e.Message);
                throw e;
            }
        }
        public object ExecuteScalar(string strsql)
        {
            try
            {
                return _database.ExecuteScalar(strsql);
            }
              catch (Exception e)
              {
                  Log.WriteError(strsql);
                  Log.WriteError(e.Message);
                  throw e;
              }
        }

        //public void BeginTransaction()
        //{
        //    _database.BeginTransaction();
        //}

        //public void Rollback()
        //{
        //    _database.Rollback();
        //}
        //public void Commit()
        //{
        //    _database.Commit();
        //}

        #region 出乘

        /// <summary>
        /// 出乘
        /// </summary>
        public void ExecPro(DateTime plan_date, string trainroadid)
        {

            try
            {
                string strSql = ExecProSql(plan_date, trainroadid);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 出乘sql
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ExecProSql(DateTime plan_date, string trainroadid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec dbo.p_create_run '" + plan_date + "'," + trainroadid);
            return strSql.ToString();
        }

        #endregion

        #region 更新交路计划标记，标记为出乘
        /// <summary>
        /// 更新交路计划标记，标记为出乘
        /// </summary>
        public void UpdatePlanOnDuty(TrainRoadPlanEntity model)
        {

            try
            {
                string strSql = UpdatePlanOnDutySql(model);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 更新交路计划标记Sql
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string UpdatePlanOnDutySql(TrainRoadPlanEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_train_road_plan set ");
            strSql.Append("int_flag = 1 ");
            strSql.Append(" where train_road_id= '" + model.TrainRoadId + "'");
            strSql.Append(" and  plan_date= '" + model.Plandate + "'");

            return strSql.ToString();
        }

        #endregion

        #region 退乘

        /// <summary>
        /// 退乘
        /// </summary>
        /// <param name="model"></param>
        public void UpdateStatus(TrainRoadPlanEntity model)
        {
            try
            {
                string strSql = UpdateStatusSql(model);
                ExecuteNonQuery(strSql);
            }
            catch (Exception e)
            {

            }
        }
        /// <summary>
        /// 退乘SQL
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string UpdateStatusSql(TrainRoadPlanEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec p_update_run '" + model.Plandate + "','" + model.TrainRoadId + "'");

            return strSql.ToString();
        }

        #endregion

        #region 设计途经站信息
        /// <summary>
        /// 设计途经站信息
        /// </summary>
        /// <param name="fromstation"></param>
        /// <param name="tostation"></param>
        /// <param name="id"></param>
        public void SetBetween(StationEntity fromstation, StationEntity tostation, int machineid)
        {
            try
            {
                string strSql = SetBetweenSql(fromstation, tostation, machineid);
                ExecuteNonQuery(strSql);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fromstation"></param>
        /// <param name="tostation"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string SetBetweenSql(StationEntity fromstation, StationEntity tostation,int machineid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_train_run set ");
            strSql.Append(" start_area_id = '" + fromstation.Id + "'");
            strSql.Append(",end_area_id = '" + tostation.Id + "'");
            strSql.Append(",update_user = 0");
            strSql.Append(",update_datetime = getdate() ");
            strSql.Append(" where machine_id = '" + machineid + "'  and status= 1 and end_time > getdate() and delete_flag = 0");

            return strSql.ToString();
        }
        #endregion

        #region 设置当前车次
        /// <summary>
        /// 设置当前车次
        /// </summary>
        /// <param name="fromstation"></param>
        /// <param name="tostation"></param>
        /// <param name="id"></param>
        public void SetCurrentTrainCode(string strtraincode, int machineid)
        {
            try
            {
                string strSql = SetCurrentTrainCodeSql(strtraincode, machineid);
                ExecuteNonQuery(strSql);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fromstation"></param>
        /// <param name="tostation"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string SetCurrentTrainCodeSql(string strtraincode, int machineid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_train_run set ");
            strSql.Append(" train_code = '" + strtraincode + "'");
            strSql.Append(",update_user = 0");
            strSql.Append(",update_datetime = getdate() ");
            strSql.Append(" where machine_id = '" + machineid + "' and status= 1 and end_time > getdate() and delete_flag = 0");


            return strSql.ToString();
        }
        #endregion


        #region 设置列车到达车站的信息

        /// <summary>
        /// 设置列车到达车站的信息
        /// </summary>
        public void UpdateTrainStation(TrainRunTrainStationEntity info)
        {
            try
            {
                ExecuteNonQuery(UpdateTrainStationSql(info));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string UpdateTrainStationSql(TrainRunTrainStationEntity info)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append("UPDATE t_train_run_train_station  SET ");
            sbsql.Append(" update_user= '" + info.update_user.Userid + "'");
            if (info.New_Arrdatetime != DateTime.MinValue)
            {
                sbsql.Append(",arr_datetime = '" + info.New_Arrdatetime.ToString() + "'");
            }
            if (info.New_Depdatetime != DateTime.MinValue)
            {
                sbsql.Append(",dep_datetime= '" + info.New_Depdatetime.ToString() + "'");
            }

            sbsql.Append(",update_datetime= getdate()");
            sbsql.Append(" where station_id='" + info.Station.Id + "'");
            sbsql.Append(" and train_run_train_id='" + info.Train.Train_run_train_id + "'");
            return sbsql.ToString();
        }
        #endregion

        #region 设置列车到达终点站站的信息

        /// <summary>
        /// 设置列车到达车站的信息
        /// </summary>
        public void SetTrainEndStation(TrainRunTrainEntity info)
        {
            try
            {
                ExecuteNonQuery(SetTrainEndStationSql(info));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string SetTrainEndStationSql(TrainRunTrainEntity info)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append("UPDATE t_train_run_train  SET ");
            if (info.update_user == null)
            {
                sbsql.Append(" update_user= NULL");
            }
            else
            {
                sbsql.Append(" update_user= '" + info.update_user.Userid + "'");
            }
            if (info.End_datetime != DateTime.MinValue)
            {
                sbsql.Append(",end_datetime = '" + info.End_datetime.ToString() + "'");
                sbsql.Append(",latetime = datediff(mi,'"+ info.End_odatetime + "','" + info.End_datetime.ToString() + "')");
            }
            sbsql.Append(",status = '" + info.Status.ToString() + "'");
            sbsql.Append(",update_datetime= getdate()");
            sbsql.Append(" where train_run_train_id='" + info.Train_run_train_id + "'");
            return sbsql.ToString();
        }
        #endregion


        #region 设置设备状态

        /// <summary>
        /// 设置设备状态
        /// </summary>
        public void SetMachineStatus(int machineid, int status)
        {
            try
            {
                string strSql = SetMachineStatusSql(machineid, status);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string SetMachineStatusSql(int machineid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  t_train_location set");
            strSql.Append(" status = '" + status + "'");
            strSql.Append(" where machine_id = '" + machineid + "'");
            return strSql.ToString();
        }

        #endregion

        #region 设置列车状态

        /// <summary>
        /// 设置列车状态
        /// </summary>
        /// <param name="trainrunid"></param>
        /// <param name="trainid"></param>
        /// <param name="status"></param>
        public void SetTrainStatus(long trainruntrainid, int status)
        {
            try
            {
                string strSql = SetTrainStatusSql(trainruntrainid, status);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                Log.WriteError(e.Message);
                throw e;
            }
        }

        private string SetTrainStatusSql(long trainruntrainid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  t_train_run_train set");
            strSql.Append(" status = '" + status + "'");
            strSql.Append(" where train_run_train_id = '" + trainruntrainid + "'");
            return strSql.ToString();
        }

        #endregion


        #region 设置当前列车

        public void SetCurrentTrain(long machineid, DateTime dtplandate, long trainruntrainid)
        {
            try
            {
                string strSql = SetCurrentTrainSql(machineid,dtplandate, trainruntrainid);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                Log.WriteError(e.Message);
                throw e;
            }
        }

        private string SetCurrentTrainSql(long machineid, DateTime dtplandate, long trainruntrainid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  t_train_run set");
            strSql.Append(" train_run_train_id = '" + trainruntrainid + "'");
            strSql.Append(",update_datetime = getdate()");
            strSql.Append(" where delete_flag = 0 and machine_id = '" + machineid + "' and  run_date = '" + dtplandate + "'");
            return strSql.ToString();
        }

        #endregion

        #region 得到下一个列车

        public TrainRunTrainEntity GetNextTrain(long trainrunid)
        {
            IDataReader dataReader = null;
            TrainRunTrainEntity model = new TrainRunTrainEntity();
            try
            {
                string strSql = GetNextTrainSql(trainrunid);
                using (dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBind(dataReader);
                    }

                }
                return model;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
                throw ex;
            }
        }


        private string GetNextTrainSql(long trainrunid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1");
            strSql.Append(" train_run_train_id ");
            strSql.Append(",train_id ");
            strSql.Append(" from t_train_run_train");
            strSql.Append(" where train_run_id = '" + trainrunid + "'");
            strSql.Append(" and (status = 0 or status is null)");
            strSql.Append(" order by seq");
            return strSql.ToString();
        }

        private TrainRunTrainEntity ReadBind(IDataReader dataReader)
        {
            TrainRunTrainEntity te = new TrainRunTrainEntity();
            te.Train_run_train_id = CommonDBCheck.ToInt(dataReader["train_run_train_id"]);
            te.Train.Train_id = CommonDBCheck.ToInt(dataReader["train_id"]);
            return te;
        }

        #endregion



        #region 获取全部交路车次

        /// <summary>
        /// 获取全部交路车次
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTrainCode()
        {
            try
            {
                string strSql = GetAllTrainCodeSQL();
                return ExecuteDataSet(strSql.ToString()).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 获取全部交路车次
        /// </summary>
        /// <returns></returns>
        public string GetAllTrainCodeSQL()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append(" t_train_road.train_road_id as id");
            strSql.Append(",dbo.f_get_TrainCode(t_train_road.train_code) as name");
            strSql.Append(",t_train_road.train_fleet_id");
            strSql.Append(",dbo.f_get_trainfleet(t_train_road.train_fleet_id) as train_fleet_name");
            strSql.Append(",dbo.f_get_class(t_train_road.train_road_id,2,0) as class_code ");
            strSql.Append(",dbo.f_get_class(t_train_road.train_road_id,2,1) as class_name ");
            strSql.Append(",dbo.f_get_TrainCode(start_train_id) as start_train_name ");
            strSql.Append(",dbo.f_get_TrainCode(end_train_id)   as end_train_name ");

            strSql.Append(" from t_train_road ");
            strSql.Append(" where t_train_road.delete_flag = 0  ");

            return strSql.ToString();
        }

        #endregion

        #region 得到所有的车站

        public DataTable GetAllStation()
        {
            try
            {
                return ExecuteDataSet(GetAllStationSql()).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GetAllStationSql()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" train_road_code");
            strSql.Append(",station_id");
            strSql.Append(",station");
            strSql.Append(",Longitude");
            strSql.Append(",Latitude");
            strSql.Append(",point_x");
            strSql.Append(",point_y");
            strSql.Append(" from v_train_road_station");
            strSql.Append(" where point_x is not null and point_y is not null");
            strSql.Append(" order by train_road_code,seq");
            return strSql.ToString();
        }
        #endregion

        #region 通过设备id得到列车信息

        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TrainInfoEntity> GetInfoByMachine(long machineid)
        {
            List<TrainInfoEntity> model = new List<TrainInfoEntity>();
            try
            {
                string strSql = GetInfoByMachineSql(machineid);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindTrainInfo(dataReader));
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetInfoByMachineSql(long ID)
        {
            string strtime = ConfigurationSettings.AppSettings["displaytime"];
            int nstarttime = 0;
            int nendtime = 0;
            try
            {
                nendtime = int.Parse(strtime);
                nstarttime = 0 - nendtime;
            }
            catch
            {
                nendtime = 0;
                nstarttime = 0;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" distinct");
            strSql.Append(" train_road_code");
            strSql.Append(",train_body_name");
            strSql.Append(",train_code ");
            strSql.Append(",start_datetime ");
            strSql.Append(",end_datetime ");
            strSql.Append(",start_odatetime ");
            strSql.Append(",end_odatetime ");
            strSql.Append(",latetime ");
            strSql.Append(",v_train_run.machine_id");
            strSql.Append(",v_train_run.machine_android_id");
            strSql.Append(",train_id");
            strSql.Append(",start_train_id");
            strSql.Append(",end_train_id");
            strSql.Append(",dbo.f_get_leader_machine(v_train_run.machine_id,0) train_lead");
            strSql.Append(",group_order");
            strSql.Append(",group_count");
            strSql.Append(",train_group_type_name");
            strSql.Append(",dbo.f_get_trainOrder_now(train_id,start_datetime) train_group_type_name_new");
            strSql.Append(",broad_user");
            strSql.Append(",diner_lead");
            strSql.Append(",start_area + '--' + end_area betweenstation");
            strSql.Append(",class_name");
            strSql.Append(",fleet_name");
            strSql.Append(",dining_class_name");
            strSql.Append(",clead");
            strSql.Append(",train_run_train_id");
            strSql.Append(",train_id");
            strSql.Append(",llead");
            strSql.Append(",train_run_tim");
            strSql.Append(",run_date");
            strSql.Append(",seq");
            strSql.Append(" from v_train_run,t_train_location");
            strSql.Append(" where v_train_run.machine_id ='" + ID + "'");
            strSql.Append(" and v_train_run.machine_id = t_train_location.machine_id ");
            strSql.Append(" and v_train_run.status = 1 ");
            strSql.Append(" and (t_train_location.status = 1 or t_train_location.status = 2) ");
            //strSql.Append(" and (t_train_location.status = 1 or (t_train_location.status = 2 and getdate()> DATEADD(mi," + nstarttime + ",start_datetime) and  getdate() < DATEADD(mi," + nendtime + ",end_datetime) )) ");
            strSql.Append(" and getdate()> DATEADD(mi," + nstarttime + ",start_datetime)  ");
            strSql.Append(" order by run_date desc,seq  desc");
            return strSql.ToString();
        }

        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainInfoEntity ReadBindTrainInfo(IDataReader dataReader)
        {
            TrainInfoEntity pe = new TrainInfoEntity();
           // pe.Id = CommonDBCheck.ToLong(dataReader["train_run_id"]);

            string strcode = CommonDBCheck.ToString(dataReader["train_road_code"]);
            if (strcode.Length > 0)
            {
                pe.Train = new List<TrainEntity>();
                string[] scode = strcode.Split('>');
                for (int i = 0; i < scode.Length; i++)
                {
                    TrainEntity te = GetTrainInfoByCode(scode[i]);
                    pe.Train.Add(te);
                }
            }
            pe.Machine = new MachineEntity();
            pe.Machine.Machine_id = CommonDBCheck.ToInt(dataReader["machine_id"]);
            pe.AndroidMachine = new MachineEntity();
            pe.AndroidMachine.Machine_id = CommonDBCheck.ToInt(dataReader["machine_android_id"]);
            object obj;
            pe.CurrentTrainCode = CommonDBCheck.ToString(dataReader["train_code"]);
            pe.StartTrain = GetTrainInfo(CommonDBCheck.ToInt(dataReader["start_train_id"]));
            pe.EndTrain = GetTrainInfo(CommonDBCheck.ToInt(dataReader["end_train_id"]));

            obj = dataReader["train_lead"];
            if (obj != null && obj != DBNull.Value)
            {
                string strlead = obj.ToString();
                string[] lead = strlead.Split(',');
                pe.Leadcollection = new List<UserEntity>();
                for (int i = 0; i < lead.Length; i++)
                {
                    UserEntity user = new UserEntity();
                    user = GetUserInfo(int.Parse(lead[i]));
                    pe.Leadcollection.Add(user);

                }
            }

            obj = dataReader["train_run_tim"];
            if (obj != null && obj != DBNull.Value)
            {
                string strlead = obj.ToString();
                string[] lead = strlead.Split(',');
                pe.TimUserCollection = new List<UserEntity>();
                for (int i = 0; i < lead.Length; i++)
                {
                    UserEntity user = new UserEntity();
                    user = GetUserInfo(int.Parse(lead[i]));
                    pe.TimUserCollection.Add(user);
                }
            }

            pe.FleetClass.Name = CommonDBCheck.ToString(dataReader["class_name"]);
            pe.TrainGroupCount = CommonDBCheck.ToInt(dataReader["group_count"]);
            pe.DinerClass.Name = CommonDBCheck.ToString(dataReader["dining_class_name"]);
            pe.TrainGroupTypeName = CommonDBCheck.ToString(dataReader["train_group_type_name_new"]);
            pe.TrainGroupTypeNameOld = CommonDBCheck.ToString(dataReader["train_group_type_name"]);
            pe.Between = CommonDBCheck.ToString(dataReader["betweenstation"]);
            pe.Broad.Userid = CommonDBCheck.ToInt(dataReader["broad_user"]);
            pe.Dinerlead.Userid = CommonDBCheck.ToInt(dataReader["diner_lead"]);
            pe.TrainFleet.Name = CommonDBCheck.ToString(dataReader["fleet_name"]);
            pe.StartTrain = pe.StartTrain;
            pe.EndTrain = pe.EndTrain;
            pe.CurrentRunTrain.Train_run_train_id = CommonDBCheck.ToInt(dataReader["train_run_train_id"]);
            pe.CurrentRunTrain.Train = GetTrainInfo(CommonDBCheck.ToInt(dataReader["train_id"]));
            pe.CurrentRunTrain.Train.TrainStation = GetTrainAllStationInfo(pe.CurrentRunTrain.Train_run_train_id, pe.CurrentRunTrain.Train);
            pe.CurrentRunTrain.Start_datetime = CommonDBCheck.ToDateTime(dataReader["start_datetime"]);
            pe.CurrentRunTrain.End_datetime = CommonDBCheck.ToDateTime(dataReader["end_datetime"]);
            pe.CurrentRunTrain.Start_odatetime = CommonDBCheck.ToDateTime(dataReader["start_odatetime"]);
            pe.CurrentRunTrain.End_odatetime = CommonDBCheck.ToDateTime(dataReader["end_odatetime"]);
            int latetime = CommonDBCheck.ToInt(dataReader["latetime"]);
            if (latetime != 0)
            {
                pe.CurrentRunTrain.Late_hour = (latetime / 60).ToString();
                pe.CurrentRunTrain.Late_minute = (latetime % 60).ToString();
            }
            pe.CLead = CommonDBCheck.ToString(dataReader["clead"]);
            pe.LLead = CommonDBCheck.ToString(dataReader["llead"]);
            pe.Plandate = CommonDBCheck.ToDateTime(dataReader["run_date"]);
            return pe;
        }
        #endregion


        #region 通过设备id得到列车信息

        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TrainInfoEntity> GetTrainInfoByMachine(long machineid)
        {
            List<TrainInfoEntity> model = new List<TrainInfoEntity>();
            try
            {
                string strSql = GetTrainInfoByMachineSql(machineid);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindTrainInfoByMachine(dataReader));
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetTrainInfoByMachineSql(long ID)
        {
            string strtime = ConfigurationSettings.AppSettings["displaytime"];
            int nstarttime = 0;
            int nendtime = 0;
            try
            {
                nendtime = int.Parse(strtime);
                nstarttime = 0 - nendtime;
            }
            catch
            {
                nendtime = 0;
                nstarttime = 0;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" train_run_id");
            strSql.Append(",dbo.f_get_trainroad(train_road_id) train_road_code");
            strSql.Append(",train_code ");
            strSql.Append(",start_time ");
            strSql.Append(",end_time ");
            strSql.Append(",start_otime ");
            strSql.Append(",end_otime ");
            strSql.Append(",t_train_run.machine_id");
            strSql.Append(",t_train_run.machine_android_id");
            strSql.Append(",train_id");
            strSql.Append(",start_train_id");
            strSql.Append(",end_train_id");
            strSql.Append(",train_lead_id");
            strSql.Append(",end_train_id");
            strSql.Append(",dbo.f_get_leader_machine(t_train_run.machine_id,0) train_leads");
            strSql.Append(",group_order");
            strSql.Append(",group_count");
            strSql.Append(",train_group_type_name");
            //strSql.Append(",dbo.f_get_trainOrder_now(train_id,start_datetime) train_group_type_name_new");
            strSql.Append(",broad_user");
            strSql.Append(",diner_lead");
            strSql.Append(",dbo.f_get_station_name(start_area_id) + '--' + dbo.f_get_station_name(end_area_id) betweenstation");
            strSql.Append(",class_id");
            strSql.Append(",dbo.f_get_class_name(class_id) class_name");
            strSql.Append(",train_fleet_id");
            strSql.Append(",dbo.f_get_trainfleet(train_fleet_id) train_fleet_name");
            strSql.Append(",dining_class_id");
            strSql.Append(",dbo.f_get_class_name(dining_class_id) dining_class_name");
            strSql.Append(",clead");
            strSql.Append(",train_run_train_id");
            strSql.Append(",train_id");
            strSql.Append(",llead");
            strSql.Append(",train_run_tim");
            strSql.Append(",run_date");
           // strSql.Append(",seq");
            strSql.Append(" from t_train_run,t_train_location");
            strSql.Append(" where t_train_run.machine_id ='" + ID + "'");
            strSql.Append(" and t_train_run.machine_id = t_train_location.machine_id ");
            strSql.Append(" and t_train_run.status = 1 ");
            strSql.Append(" and (t_train_location.status = 1 or t_train_location.status = 2) ");
            strSql.Append(" and getdate()> DATEADD(mi," + nstarttime + ",start_time)  ");
            strSql.Append(" order by train_run_id,run_date desc");
            return strSql.ToString();
        }

        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainInfoEntity ReadBindTrainInfoByMachine(IDataReader dataReader)
        {
            TrainInfoEntity pe = new TrainInfoEntity();
            pe.Id = CommonDBCheck.ToLong(dataReader["train_run_id"]);

            string strcode = CommonDBCheck.ToString(dataReader["train_road_code"]);
            if (strcode.Length > 0)
            {
                pe.Train = new List<TrainEntity>();
                string[] scode = strcode.Split('>');
                for (int i = 0; i < scode.Length; i++)
                {
                    TrainEntity te = GetTrainInfoByCode(scode[i]);
                    pe.Train.Add(te);
                }
            }
            pe.BeginDate = CommonDBCheck.ToDateTime(dataReader["start_time"]);
            pe.EndDate = CommonDBCheck.ToDateTime(dataReader["end_time"]);
            pe.BeginDateOld = CommonDBCheck.ToDateTime(dataReader["start_otime"]);
            pe.EndDateOld = CommonDBCheck.ToDateTime(dataReader["end_otime"]);
            pe.Machine = new MachineEntity();
            pe.Machine.Machine_id = CommonDBCheck.ToInt(dataReader["machine_id"]);
            pe.AndroidMachine = new MachineEntity();
            pe.AndroidMachine.Machine_id = CommonDBCheck.ToInt(dataReader["machine_android_id"]);
            pe.CurrentTrainCode = CommonDBCheck.ToString(dataReader["train_code"]);
            pe.StartTrain = GetTrainInfo(CommonDBCheck.ToInt(dataReader["start_train_id"]));
            pe.EndTrain = GetTrainInfo(CommonDBCheck.ToInt(dataReader["end_train_id"]));
            if (pe.Lead == null)
            {
                pe.Lead = new UserEntity();
            }
            int leadid = CommonDBCheck.ToInt(dataReader["train_lead_id"]);
            pe.Lead = GetUserInfo(leadid);
            string strlead = CommonDBCheck.ToString(dataReader["train_leads"]);
            if (strlead.Length > 0)
            {
                string[] lead = strlead.Split(',');
                pe.Leadcollection = new List<UserEntity>();
                for (int i = 0; i < lead.Length; i++)
                {
                    UserEntity user = new UserEntity();
                    user = GetUserInfo(int.Parse(lead[i]));
                    pe.Leadcollection.Add(user);

                }
            }

            string strtim = CommonDBCheck.ToString(dataReader["train_run_tim"]);
            if (strtim.Length > 0)
            {
                string[] lead = strlead.Split(',');
                pe.TimUserCollection = new List<UserEntity>();
                for (int i = 0; i < lead.Length; i++)
                {
                    UserEntity user = new UserEntity();
                    user = GetUserInfo(int.Parse(lead[i]));
                    pe.TimUserCollection.Add(user);
                }
            }
            pe.FleetClass.Classid = CommonDBCheck.ToInt(dataReader["class_id"]);
            pe.FleetClass.Name = CommonDBCheck.ToString(dataReader["class_name"]);
            pe.TrainFleet.Train_fleet_id = CommonDBCheck.ToInt(dataReader["train_fleet_id"]);
            pe.TrainFleet.Name = CommonDBCheck.ToString(dataReader["train_fleet_name"]);
            pe.TrainGroupCount = CommonDBCheck.ToInt(dataReader["group_count"]);
            pe.DinerClass.Classid = CommonDBCheck.ToInt(dataReader["dining_class_id"]);
            pe.DinerClass.Name = CommonDBCheck.ToString(dataReader["dining_class_name"]);
           // pe.TrainGroupTypeName = CommonDBCheck.ToString(dataReader["train_group_type_name_new"]);
            pe.TrainGroupTypeNameOld = CommonDBCheck.ToString(dataReader["train_group_type_name"]);
            pe.Between = CommonDBCheck.ToString(dataReader["betweenstation"]);
            pe.Broad.Userid = CommonDBCheck.ToInt(dataReader["broad_user"]);
            pe.Dinerlead.Userid = CommonDBCheck.ToInt(dataReader["diner_lead"]);
            pe.CurrentRunTrain = GetTrainRunTrainInformation(CommonDBCheck.ToInt(dataReader["train_run_train_id"]));
            pe.CLead = CommonDBCheck.ToString(dataReader["clead"]);
            pe.LLead = CommonDBCheck.ToString(dataReader["llead"]);
            pe.Plandate = CommonDBCheck.ToDateTime(dataReader["run_date"]);
            return pe;
        }
        #endregion


        #region 通过设备id得到列车信息

        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TrainRunTrainEntity GetTrainRunTrainInformation(long trainruntrainid)
        {
            TrainRunTrainEntity model = new TrainRunTrainEntity();
            try
            {
                string strSql = GetTrainRunTrainInformationSql(trainruntrainid);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model = ReadBindTrainInfoTrain(dataReader);
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetTrainRunTrainInformationSql(long trainruntrainid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" train_run_id");
            strSql.Append(",train_run_train_id");
            strSql.Append(",start_datetime");
            strSql.Append(",end_datetime ");
            strSql.Append(",train_id ");
            strSql.Append(",seq ");
            strSql.Append(",start_odatetime ");
            strSql.Append(",end_odatetime ");
            strSql.Append(",latetime ");
            strSql.Append(",status");
            strSql.Append(" from t_train_run_train");
            strSql.Append(" where train_run_train_id ='" + trainruntrainid + "'");
            strSql.Append(" order by seq");
            return strSql.ToString();
        }

        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TrainRunTrainEntity> GetTrainRunTrainInfo(long trainrunid)
        {
            List<TrainRunTrainEntity> model = new List<TrainRunTrainEntity>();
            try
            {
                string strSql = GetTrainRunTrainInfoSql(trainrunid);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindTrainInfoTrain(dataReader));
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetTrainRunTrainInfoSql(long trainrunid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" train_run_id");
            strSql.Append(",train_run_train_id");
            strSql.Append(",start_datetime");
            strSql.Append(",end_datetime ");
            strSql.Append(",train_id ");
            strSql.Append(",seq ");
            strSql.Append(",start_odatetime ");
            strSql.Append(",end_odatetime ");
            strSql.Append(",latetime ");
            strSql.Append(",status");
            strSql.Append(" from t_train_run_train");
            strSql.Append(" where train_run_id ='" + trainrunid + "'");
            strSql.Append(" order by seq");
            return strSql.ToString();
        }

        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainRunTrainEntity ReadBindTrainInfoTrain(IDataReader dataReader)
        {
            TrainRunTrainEntity pe = new TrainRunTrainEntity();
            pe.Train_run_train_id = CommonDBCheck.ToLong(dataReader["train_run_train_id"]);
            pe.TrainInfo = new TrainInfoEntity();
            pe.TrainInfo.Id = CommonDBCheck.ToLong(dataReader["train_run_id"]);
            pe.Seq = CommonDBCheck.ToInt(dataReader["seq"]);
            pe.Train = GetTrainInfo(CommonDBCheck.ToInt(dataReader["train_id"]));
            pe.Train.TrainStation = GetTrainAllStationInfo(pe.Train_run_train_id,pe.Train);
            pe.Start_datetime = CommonDBCheck.ToDateTime(dataReader["start_datetime"]);
            pe.End_datetime = CommonDBCheck.ToDateTime(dataReader["end_datetime"]);
            pe.Start_odatetime = CommonDBCheck.ToDateTime(dataReader["start_odatetime"]);
            pe.End_odatetime = CommonDBCheck.ToDateTime(dataReader["end_odatetime"]);
            int latetime = CommonDBCheck.ToInt(dataReader["latetime"]);
            if (latetime != 0)
            {
                pe.Late_hour = (latetime / 60).ToString();
                pe.Late_minute = (latetime % 60).ToString();
            }
            pe.Status = CommonDBCheck.ToInt(dataReader["status"]);
            return pe;
        }
        #endregion


        #region 得到所有正在运行的设备
       
        /// <summary>
        /// 得到所有正在运行的设备
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInfo()
        {
            try
            {
                DataSet ds = ExecuteDataSet(GetAllInfoSql());
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string GetAllInfoSql()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" distinct t.train_run_id,address,train_location_id");
            strSql.Append(",case when isnull(offlongitude,'0') = '0' then longitude else offlongitude end as longitude");
            strSql.Append(",case when isnull(offlatitude,'0') = '0' then latitude else offlatitude end as latitude");
            strSql.Append(",t_train_location.point_x");
            strSql.Append(",t_train_location.point_y");
            strSql.Append(",t_train_location.flag");
            strSql.Append(",t_train_location.machine_id");
            //zyp 20130819 begin
            strSql.Append(",t.machine_android_id");
            strSql.Append(",t.class_id");
            strSql.Append(",t.run_date");
            //zyp 20130819 end
            strSql.Append(",t.train_code");
            strSql.Append(",t_train_location.current_datetime");
            strSql.Append(",t_train_location.status");
            strSql.Append(",dbo.f_get_user(train_lead,1) train_lead");
            strSql.Append(",'" + _strdatabasename + "' temp");
            strSql.Append(" from t_train_location");
            strSql.Append(" inner join (select distinct machine_id,train_run_id,train_code,run_date,class_id,machine_android_id,train_lead from v_train_run where status = 1 and getdate() between start_time and end_time) t ");
            //lhb 20151204
            //strSql.Append(" on (t.machine_id = t_train_location.machine_id)");
            strSql.Append(" on (t.run_date = t_train_location.run_datetime and t.class_id = t_train_location.classid)");
            strSql.Append(" where t_train_location.status in (1,2) ");
            return strSql.ToString();
        }
        
        #endregion

        #region 得到正在运行的设备

        /// <summary>
        /// 得到正在运行的设备
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInfo(int machineid)
        {
            try
            {
                DataSet ds = ExecuteDataSet(GetAllInfoSql(machineid));
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string GetAllInfoSql(int machineid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" train_location_id");
            strSql.Append(",t_train_location.longitude");
            strSql.Append(",t_train_location.latitude");
            strSql.Append(",t_train_location.point_x");
            strSql.Append(",t_train_location.point_y");
            strSql.Append(",t_train_location.flag");
            strSql.Append(",t_train_location.machine_id");
            strSql.Append(",t_train_location.current_datetime");
            strSql.Append(",t.train_road_code");
            strSql.Append(",t_train_location.status");
            strSql.Append(",'" + _strdatabasename + "' temp");
            strSql.Append(" from t_train_location");
            strSql.Append(" left join (select distinct machine_id,train_road_code from v_train_run where status = 1) t on (t.machine_id = t_train_location.machine_id)");
            strSql.Append(" where t_train_location.status in (1,2) and t_train_location.longitude is not null and t_train_location.latitude is not null");
            strSql.Append(" and t_train_location.machine_id= '" + machineid + "'");
            return strSql.ToString();
        }

        #endregion


        #region 得到列车的到站信息

        /// <summary>
        /// 得到列车运行时的车站信息
        /// </summary>
        /// <returns></returns>
        public List<TrainRunTrainStationEntity> GetInfo(long trainruntrainid)
        {
            try
            {
                List<TrainRunTrainStationEntity> model = new List<TrainRunTrainStationEntity>();
                string strSql = GetInfoSql(trainruntrainid);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindStation(dataReader));
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GetInfoSql(long trainruntrainid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select train_run_train_station_id ");
            strSql.Append(",train_run_id ");
            strSql.Append(",train_run_train_id");
            strSql.Append(",t_train_run_train_station.station_id");
            strSql.Append(",seq");
            strSql.Append(",arr_datetime");
            strSql.Append(",dep_datetime");
            strSql.Append(",arr_odatetime");
            strSql.Append(",dep_odatetime");
            strSql.Append(",dep_trainnum");
            strSql.Append(",arr_trainnum");
            strSql.Append(",t_base_station.station_name station_name");
            strSql.Append(",t_base_station.longitude longitude");
            strSql.Append(",t_base_station.latitude latitude");
            strSql.Append(",t_base_station.point_x");
            strSql.Append(",t_base_station.point_y");
            strSql.Append(" from t_train_run_train_station");
            strSql.Append(" left join t_base_station");
            strSql.Append(" on (t_base_station.station_id = t_train_run_train_station.station_id)");
            strSql.Append(" where 1=1 ");
            strSql.Append(" and train_run_train_id = '" + trainruntrainid + "'");
            return strSql.ToString();
        }

        private TrainRunTrainStationEntity ReadBindStation(IDataReader dataReaderTrain)
        {
            TrainRunTrainStationEntity mode = new TrainRunTrainStationEntity();
            mode.Station.Id = CommonDBCheck.ToInt(dataReaderTrain["station_id"]);
            mode.Station.Name = CommonDBCheck.ToString(dataReaderTrain["station_name"]);
            mode.Station.Position.Longitude = CommonDBCheck.ToDouble(dataReaderTrain["longitude"]);
            mode.Station.Position.Latitude = CommonDBCheck.ToDouble(dataReaderTrain["latitude"]);
            mode.Station.Position.X = CommonDBCheck.ToInt(dataReaderTrain["point_x"]);
            mode.Station.Position.Y = CommonDBCheck.ToInt(dataReaderTrain["point_y"]);
            mode.Seq = CommonDBCheck.ToInt(dataReaderTrain["seq"]);
            mode.Arrdatetime = CommonDBCheck.ToDateTime(dataReaderTrain["arr_datetime"]);
            mode.Depdatetime = CommonDBCheck.ToDateTime(dataReaderTrain["dep_datetime"]);
            mode.Old_Arrdatetime = CommonDBCheck.ToDateTime(dataReaderTrain["arr_odatetime"]);
            mode.Old_Depdatetime = CommonDBCheck.ToDateTime(dataReaderTrain["dep_odatetime"]);
            mode.Train = new TrainRunTrainEntity();
            mode.Train.Train_run_train_id = CommonDBCheck.ToInt(dataReaderTrain["train_run_train_id"]);
            mode.Dep_trainnum = CommonDBCheck.ToString(dataReaderTrain["dep_trainnum"]);
            mode.Arr_trainnum = CommonDBCheck.ToString(dataReaderTrain["arr_trainnum"]);
            return mode;
        }
        #endregion


        public StationMapInfoEntity GetStartStation(string strtraincode,DateTime plandate)
        {
            IDataReader dataReader = null;
            StationMapInfoEntity model = new StationMapInfoEntity();
            try
            {
                string strSql = GetStartStationSql(strtraincode, plandate);
                using (dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBindStationPosition(dataReader);
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        private string GetStartStationSql(string strtraincode,DateTime plandate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" train_road_code");
            strSql.Append(",station_id");
            strSql.Append(",station");
            strSql.Append(",Longitude");
            strSql.Append(",Latitude");
            strSql.Append(",point_x");
            strSql.Append(",point_y");
            strSql.Append(" from t_train_road_smallstation");
            strSql.Append(" where point_x is not null and point_y is not null");
            strSql.Append(" and station in (select dbo.f_get_station_name(start_station) from t_train where train_road_code = '" + strtraincode + "')");
            strSql.Append(" and train_road_date = '" + plandate + "'");

            return strSql.ToString();
        }

        /// <summary>
        /// 通过经纬度得到最近的车站信息
        /// </summary>
        /// <param name="strtrainroadcode"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public StationMapInfoEntity GetNearlestStation(string strtrainroadcode, double Longitude, double Latitude, DateTime plandate)
        {
            StationMapInfoEntity model = new StationMapInfoEntity();
            try
            {
                string strSql = GetNearlestStationSql(strtrainroadcode, Longitude, Latitude, plandate);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBindStationPosition(dataReader);
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        private string GetNearlestStationSql(string strtraincode, double Longitude, double Latitude, DateTime plandate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1");
            strSql.Append(" train_road_code");
            strSql.Append(",station");
            strSql.Append(",station_id");
            strSql.Append(",Longitude");
            strSql.Append(",Latitude");
            strSql.Append(",point_x");
            strSql.Append(",point_y");
            strSql.Append(",sqrt(power(" + Longitude + "-Longitude,2) +  power(" + Latitude + "-Latitude,2))");
            strSql.Append(" from t_train_road_smallstation");
            strSql.Append(" where train_road_code = '" + strtraincode + "' and point_x is not null and longitude is not null");
            strSql.Append(" and train_road_date = '" + plandate + "'");
            strSql.Append(" order by sqrt(power(" + Longitude + "-Longitude,2) +  power(" + Latitude + "-Latitude,2))");
            return strSql.ToString();
        }



        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private StationMapInfoEntity ReadBindStationPosition(IDataReader dataReader)
        {
            StationMapInfoEntity model = new StationMapInfoEntity();
            //车站
            model.Station.Name = CommonDBCheck.ToString(dataReader["station"]);
            //车站id
            model.Station.Id = CommonDBCheck.ToInt(dataReader["station_id"]);
            //纬度
            model.Position.Latitude = CommonDBCheck.ToDouble(dataReader["Latitude"]);
            //经度
            model.Position.Longitude = CommonDBCheck.ToDouble(dataReader["Longitude"]); 
    
            model.Position.X = CommonDBCheck.ToInt(dataReader["point_x"]);
            model.Position.Y = CommonDBCheck.ToInt(dataReader["point_y"]); 

            return model;
        }



        /// <summary>
        /// 通过经纬度得到最近的车站信息
        /// </summary>
        /// <param name="strtrainroadcode"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public StationMapInfoEntity GetNearlestStationXY(string strtrainroadcode, int x, int y, DateTime plandate)
        {
            StationMapInfoEntity model = new StationMapInfoEntity();
            try
            {
                string strSql = GetNearlestStationXYSql(strtrainroadcode, x, y, plandate);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBindStationPosition(dataReader);
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        private string GetNearlestStationXYSql(string strtraincode, int x, int y, DateTime plandate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1");
            strSql.Append(" train_road_code");
            strSql.Append(",station");
            strSql.Append(",station_id");
            strSql.Append(",Longitude");
            strSql.Append(",Latitude");
            strSql.Append(",point_x");
            strSql.Append(",point_y");
            strSql.Append(",sqrt(power(" + x + "-point_x,2) +  power(" + y + "-point_y,2))");
            strSql.Append(" from t_train_road_smallstation");
            strSql.Append(" where train_road_code = '" + strtraincode + "' and point_x is not null and longitude is not null");
            strSql.Append(" and train_road_date = '" + plandate + "'");
            strSql.Append(" order by sqrt(power(" + x + "-point_x,2) +  power(" + y + "-point_y,2))");
            return strSql.ToString();
        }


        #region 得到信息

        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="train_id"></param>
        public TrainEntity GetTrainInfoByCode(string train_id)
        {
            IDataReader dataReader = null;
            TrainEntity model = new TrainEntity();
            try
            {
                string strSql = GetTrainInfoByCodeSQL(train_id);
                using (dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBindTrainInformation(dataReader);
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获得某ID数据
        /// </summary>
        /// <param name="intwhere"></param>
        /// <returns></returns>
        private string GetTrainInfoByCodeSQL(string longwhere)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select train_id,");
            strsql.Append("train_code,");
            strsql.Append("train_type,");
            strsql.Append("dbo.f_get_param(train_type,1) as train_type_name,");
            strsql.Append("isnull(start_station,0) as start_station,");
            strsql.Append("isnull(end_station,0) as end_station,");
            strsql.Append("dbo.f_get_station_name(case when start_station LIKE '%[^0-9]%' then '' else start_station end) start_station_name,");
            strsql.Append("dbo.f_get_station_name(case when end_station LIKE '%[^0-9]%' then '' else end_station end) end_station_name,");
            strsql.Append("isnull(start_time,'') as start_time,");
            strsql.Append("isnull(end_time,'') as end_time,");
            strsql.Append("isnull(relativeday,0) as relativeday,");
            strsql.Append("isnull(work_km,0) as work_km,");
            strsql.Append("direction,");
            strsql.Append("dbo.f_get_direction_name(direction) as direction_name,");
            strsql.Append("line_name,");
            strsql.Append("dbo.f_get_railway_name(line_name) as line_name_name ");
            strsql.Append(",stop_station");
            strsql.Append(" from t_train ");
            strsql.Append("where train_code = '" + longwhere + "'" );

            return strsql.ToString();
        }

        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="train_id"></param>
        public TrainEntity GetTrainInfo(long train_id)
        {
            IDataReader dataReader = null;
            TrainEntity model = new TrainEntity();
            try
            {
                string strSql = GetTrainInfoSQL(train_id);
                using (dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBindTrainInformation(dataReader);
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获得某ID数据
        /// </summary>
        /// <param name="intwhere"></param>
        /// <returns></returns>
        private string GetTrainInfoSQL(long longwhere)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select train_id,");
            strsql.Append("train_code,");
            strsql.Append("train_type,");
            strsql.Append("dbo.f_get_param(train_type,1) as train_type_name,");
            strsql.Append("isnull(start_station,0) as start_station,");
            strsql.Append("isnull(end_station,0) as end_station,");
            strsql.Append("dbo.f_get_station_name(case when start_station LIKE '%[^0-9]%' then '' else start_station end) start_station_name,");
            strsql.Append("dbo.f_get_station_name(case when end_station LIKE '%[^0-9]%' then '' else end_station end) end_station_name,");
            strsql.Append("isnull(start_time,'') as start_time,");
            strsql.Append("isnull(end_time,'') as end_time,");
            strsql.Append("isnull(relativeday,0) as relativeday,");
            strsql.Append("isnull(work_km,0) as work_km,");
            strsql.Append("direction,");
            strsql.Append("dbo.f_get_direction_name(direction) as direction_name,");
            strsql.Append("line_name,");
            strsql.Append("dbo.f_get_railway_name(line_name) as line_name_name ");
            strsql.Append(",stop_station");
            
            strsql.Append(" from t_train ");
            strsql.Append("where train_id = " + longwhere);

            return strsql.ToString();
        }
        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainEntity ReadBindTrainInformation(IDataReader dataReader)
        {
            TrainEntity model = new TrainEntity();
            //主键
            model.Train_id = CommonDBCheck.ToInt(dataReader["train_id"]);
            //车次
            model.Train_code = CommonDBCheck.ToString(dataReader["train_code"]);
            //列车类型
            model.Train_type.para_code = CommonDBCheck.ToString(dataReader["train_type"]);
            model.Train_type.para_name = CommonDBCheck.ToString(dataReader["train_type_name"]);

            //始发站
            model.Start_Station.Id = CommonDBCheck.ToInt(dataReader["start_station"]);
            model.Start_Station.Name = CommonDBCheck.ToString(dataReader["start_station_name"]);
            //终到站
            model.End_Station.Id = CommonDBCheck.ToInt(dataReader["end_station"]);
            model.End_Station.Name = CommonDBCheck.ToString(dataReader["end_station_name"]);
            //始发时间
            model.Start_time = CommonDBCheck.ToString(dataReader["start_time"]).Trim();
            //终到时间
            model.End_time = CommonDBCheck.ToString(dataReader["end_time"]).Trim();
            //相对始发站天数
            model.Relativeday = CommonDBCheck.ToInt(dataReader["relativeday"]);
            //里程
            model.Work_km = CommonDBCheck.ToInt(dataReader["work_km"]);
            //方向
            model.Direction.Id = CommonDBCheck.ToInt(dataReader["direction"]);
            model.Direction.name = CommonDBCheck.ToString(dataReader["direction_name"]);
            //线名
            model.RailWay.Id = CommonDBCheck.ToInt(dataReader["line_name"]);
            model.RailWay.Name = CommonDBCheck.ToString(dataReader["line_name_name"]);
            model.Stop_station = CommonDBCheck.ToString(dataReader["stop_station"]);
            return model;
        }
        #endregion
        #region
        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="train_id"></param>
        public List<TrainStationEntity> GetTrainAllStationInfo(long train_run_train_id,TrainEntity train)
        {
            IDataReader dataReader = null;
            List<TrainStationEntity> model = new List<TrainStationEntity>();
            try
            {
                string strSql = GetTrainAllStationInfoSql(train_run_train_id);
                TrainStationEntity tse = new TrainStationEntity();

                TrainStationEntity trainse = new TrainStationEntity();
                string strarrtime= string.Empty;
                using (dataReader = ExecuteReader(strSql))
                {
                    int nday = 0;
                    int nrday = 0;
                    while(dataReader.Read())
                    {
                        trainse = ReadBind(dataReader, strarrtime, nday,nrday);
                        trainse.Train = train;
                        model.Add(trainse);
                        strarrtime = trainse.Arr_time;
                        if (strarrtime.Trim().Length == 0)
                        {
                            strarrtime = trainse.Dep_time;
                        }
                        nday = trainse.Relativeday;
                        nrday = trainse.Dep_Relativeday;
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GetTrainAllStationInfoSql(long train_run_train_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("t_train_run_train_station.station_id train_station_id,");
         //   strSql.Append("train_id,");
            strSql.Append("t_train_run_train_station.seq station_seq,");
            strSql.Append("t_base_station.station_id station_code,");
            strSql.Append("t_base_station.station_name station_name,");
        //    strSql.Append("isnull(arr_relativeday,0) as arr_relativeday,");
            strSql.Append("isnull(t_train_run_train_station.arr_datetime,'') as arr_time,");
            strSql.Append("isnull(t_train_run_train_station.dep_datetime,'') as dep_time,");
         //   strSql.Append("isnull(station_rtype,0) as station_rtype,");
            strSql.Append("t_base_station.longitude longitude,");
            strSql.Append("t_base_station.latitude latitude");
            strSql.Append(" from t_train_run_train_station ");
            strSql.Append(" left join t_base_station  on (t_base_station.station_id = t_train_run_train_station.station_id)");
            strSql.Append("where train_run_train_id = " + train_run_train_id);
            strSql.Append(" order by station_seq");
            return strSql.ToString();
        }

        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        private TrainStationEntity ReadBind(IDataReader dataReader,string dtstarttime,int nday,int nrday)
        {
            TrainStationEntity model = new TrainStationEntity();
            //主键
            model.Train_Station_Id = CommonDBCheck.ToInt(dataReader["train_station_id"]);
            //train_id
        //    model.Train.Train_id = CommonDBCheck.ToInt(dataReader["train_id"]);
            //站序
            model.Station_Seq = CommonDBCheck.ToInt(dataReader["station_seq"]);
            //站名
            model.Station.Id = CommonDBCheck.ToInt(dataReader["station_code"]);
            model.Station.Name = CommonDBCheck.ToString(dataReader["station_name"]);
            model.Station.Position.Latitude = CommonDBCheck.ToDouble(dataReader["latitude"]);
            model.Station.Position.Longitude = CommonDBCheck.ToDouble(dataReader["longitude"]);
            //相对到达天数
        //    model.Relativeday = CommonDBCheck.ToInt(dataReader["arr_relativeday"]); 
            //到站时间
            DateTime dtarr = CommonDBCheck.ToDateTime(dataReader["arr_time"]);
            if (dtarr == DateTime.Parse("1900/1/1 0:00:00"))
            {
                model.Arr_time = string.Empty;

            }
            else
            {
                model.Arr_time = dtarr.ToString("HH:mm");
            }
            //出发时间
            DateTime dtdep = CommonDBCheck.ToDateTime(dataReader["dep_time"]);
            if (dtdep == DateTime.Parse("1900/1/1 0:00:00"))
            {
                model.Dep_time = string.Empty;

            }
            else
            {
                model.Dep_time = dtdep.ToString("HH:mm");
            }
            if (model.Arr_time == null || model.Arr_time.Trim().Length == 0)
            {
                if (DateTime.Parse(DateTime.Now.ToShortDateString() + " " + model.Dep_time) < DateTime.Parse(DateTime.Now.ToShortDateString() + " " + dtstarttime))
                {
                    nday = nday + 1;
                }
            }
            else
            {
                if (DateTime.Parse(DateTime.Now.ToShortDateString() + " " + model.Arr_time) < DateTime.Parse(DateTime.Now.ToShortDateString() + " " + dtstarttime))
                {
                    nday = nday + 1;
                }
            }
            if (model.Dep_time == null || model.Dep_time.Trim().Length == 0)
            {
                if (DateTime.Parse(DateTime.Now.ToShortDateString() + " " + model.Arr_time) < DateTime.Parse(DateTime.Now.ToShortDateString() + " " + dtstarttime))
                {
                    nrday = nrday + 1;
                }
            }
            else
            {
                if (DateTime.Parse(DateTime.Now.ToShortDateString() + " " + model.Dep_time) < DateTime.Parse(DateTime.Now.ToShortDateString() + " " + dtstarttime))
                {
                    nrday = nrday + 1;
                }
            }

            model.Relativeday = nday;
            model.Dep_Relativeday = nrday;
            return model;
        }
        #endregion

        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="class_id"></param>
        public UserEntity GetUserInfo(long user_id)
        {
            UserEntity model = new UserEntity();
            try
            {
                string strSql = GetUserSql(user_id);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBindUser(dataReader);
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private string GetUserSql(long userid)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append("SELECT ");
            sbsql.Append(" t_user.user_id");//主键
            sbsql.Append(" ,code");//登录代码
            sbsql.Append(" ,t_user.name"); //用户名
            sbsql.Append(" ,password");//用户密码
            sbsql.Append(" ,post_id");//岗位id
            sbsql.Append(" ,dbo.f_get_post(post_id) as post_name");
            sbsql.Append(",train_fleet_id ");
            sbsql.Append(",dbo.f_get_trainfleet(train_fleet_id) as train_fleet_name");
            sbsql.Append(",department_id ");
            sbsql.Append(",dbo.f_get_department(department_id) as department_name");
            sbsql.Append(",function_id ");
            sbsql.Append(",dbo.f_get_function(function_id) as function_name");
            sbsql.Append(" ,area_id");
            sbsql.Append(" ,dbo.f_get_area_name(area_id) as area_name");
            sbsql.Append(" ,t_user.telephone tele");//
            sbsql.Append(" ,photo ");
            sbsql.Append(" FROM t_user left join t_user_archives on (t_user.user_id = t_user_archives.user_id)");
            sbsql.Append(" WHERE t_user.user_id='" + userid + "'");
            return sbsql.ToString();
        }                            

        private UserEntity ReadBindUser(IDataReader dataReader)
        {
            UserEntity model = new UserEntity();
            if (model.UserArchives == null)
            {
                model.UserArchives = new UserArchivesEntity();
            }
            //主键
            model.Userid = CommonDBCheck.ToInt(dataReader["user_id"]);
            //用户编号
            model.Code = CommonDBCheck.ToString(dataReader["code"]);

            //用户名称
            model.Name = CommonDBCheck.ToString(dataReader["name"]);

            model.Telephone = CommonDBCheck.ToString(dataReader["tele"]);

            model.Area.area_id = CommonDBCheck.ToInt(dataReader["area_id"]);
            model.Area.area_name = CommonDBCheck.ToString(dataReader["area_name"]);

             //车队
            model.Train_fleet.Train_fleet_id = CommonDBCheck.ToInt(dataReader["train_fleet_id"]);
            model.Train_fleet.Name = CommonDBCheck.ToString(dataReader["train_fleet_name"]);

            //部门
            model.Department.Id = CommonDBCheck.ToInt(dataReader["department_id"]);
            model.Department.name = CommonDBCheck.ToString(dataReader["department_name"]);

            //岗位

            model.Function.Id = CommonDBCheck.ToInt(dataReader["function_id"]);
            model.Function.name = CommonDBCheck.ToString(dataReader["function_name"]);

            model.Post.Id = CommonDBCheck.ToInt(dataReader["post_id"]);
            model.Post.Name = CommonDBCheck.ToString(dataReader["post_name"]);

            model.UserArchives.Photo = CommonDBCheck.ToString(dataReader["photo"]);

            return model;
        }

        #region 是否存在列车
        public bool IsExistTrain(string strtraincode)
        {
            try
            {
                object obj = ExecuteScalar(IsExistTrainSql(strtraincode));

                if (obj == null)
                {
                    return false;
                }
                else
                {
                    if (((int)obj) == 0)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private string IsExistTrainSql(string strtraincode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" count(1) traincount");
            strSql.Append(" from t_train_station");
            strSql.Append(" where dep_trainnum = '" + strtraincode + "' or arr_trainnum =  '" + strtraincode + "'");
            return strSql.ToString();
        }
        #endregion

        #region 是否存在列车(交路）
        public bool IsExistTrainRoad(string strtraincode)
        {
            try
            {
                object obj = ExecuteScalar(IsExistTrainRoadSql(strtraincode));

                if (obj == null)
                {
                    return false;
                }
                else
                {
                    if (((int)obj) == 0)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private string IsExistTrainRoadSql(string strtraincode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" count(1) traincount");
            strSql.Append(" from t_train_road");
            strSql.Append(" where train_code = '" + strtraincode + "'");
            return strSql.ToString();
        }
        #endregion

        #region 得到列车编组信息
        /// <summary>
        /// 得到列车编组信息
        /// </summary>
        public List<TrainGroupUserEntity> GetTrainRunGroup(long trainrunid)
        {
            IDataReader dataReader = null;
            List<TrainGroupUserEntity> model = new List<TrainGroupUserEntity>();
            try
            {
                string strSql = GetTrainRunGroupSql(trainrunid);
                using (dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        TrainGroupUserEntity tguser = ReadBindGroup(dataReader);
                        model.Add(tguser);
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 得到列车编组信息sql
        /// </summary>
        /// <param name="trainrunid"></param>
        /// <returns></returns>
        private string GetTrainRunGroupSql(long trainrunid)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select distinct");
          //  strsql.Append(" train_run_group_id");
            strsql.Append(" train_run_id");
           // strsql.Append(",train_run_train_id");
            strsql.Append(",traincar_code");
            strsql.Append(",name");
            strsql.Append(",type");
            strsql.Append(",dbo.f_get_param(type,1) type_name");
            strsql.Append(",use_type");
            strsql.Append(",dbo.f_get_param(use_type,1) use_type_name");
            strsql.Append(",group_number");
            strsql.Append(",seat_count");
            strsql.Append(",user_id");
            strsql.Append(",user_id1");
            strsql.Append(",dbo.f_get_user(user_id,1) user_name");
            strsql.Append(",dbo.f_get_user(user_id1,1) user_name1");
            strsql.Append(",memo");
            strsql.Append(" from t_train_run_group ");
            strsql.Append(" where train_run_id = '" + trainrunid + "'");
            strsql.Append(" order by group_number");
            return strsql.ToString();
        }

        /// <summary>
        /// 得到的信息赋值
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainGroupUserEntity ReadBindGroup(IDataReader dataReader)
        {
            TrainGroupUserEntity model = new TrainGroupUserEntity();
            //主键
          //  model.Train_group_id = CommonDBCheck.ToInt(dataReader["train_run_group_id"]);

          //  model.TrainRunTrain.Train_run_train_id = CommonDBCheck.ToInt(dataReader["train_run_train_id"]);

            model.Sequence = CommonDBCheck.ToInt(dataReader["group_number"]);

            model.Name = CommonDBCheck.ToString(dataReader["name"]);

            model.Type.para_code = CommonDBCheck.ToString(dataReader["type"]);

            model.Type.para_name =  CommonDBCheck.ToString(dataReader["type_name"]);

            model.Use_type.para_code = CommonDBCheck.ToString(dataReader["use_type"]);

            model.Use_type.para_name = CommonDBCheck.ToString(dataReader["use_type_name"]);

            UserEntity u1 = new UserEntity();
            u1.Userid = CommonDBCheck.ToInt(dataReader["user_id"]);
            u1.Name = CommonDBCheck.ToString(dataReader["user_name"]);
            model.User.Add(u1);
            UserEntity u2 = new UserEntity();
            u2.Userid = CommonDBCheck.ToInt(dataReader["user_id1"]);
            u2.Name = CommonDBCheck.ToString(dataReader["user_name1"]);
            model.User.Add(u2);
            model.Seat_count = CommonDBCheck.ToInt(dataReader["seat_count"]);
            model.Traincar_code = CommonDBCheck.ToString(dataReader["traincar_code"]);
            return model;
        }
        #endregion




        #region 得到将要出乘的列车信息

        /// <summary>
        /// 得到将要出乘的列车信息
        /// </summary>
        /// <returns></returns>
        public List<TrainRoadPlanEntity> GetTrainWorkInfo(int ntime)
        {
            List<TrainRoadPlanEntity> model = new List<TrainRoadPlanEntity>();
            try
            {
                string strSql = GetTrainWorkInfoSql(ntime);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindTrainWorkInfo(dataReader));
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 得到将要出乘的列车信息Sql
        /// </summary>
        /// <param name="traincode"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private string GetTrainWorkInfoSql(int ntime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select train_road_plan_id ");
            strSql.Append(@",train_road_id");
            strSql.Append(@",plan_date ");
            strSql.Append(@" from dbo.t_train_road_plan ");
            strSql.Append(@" where status=0 and delete_flag = 0");
            strSql.Append(@" and DATEDIFF(n, start_datetime, getdate())  > " + ntime);
            return strSql.ToString();
        }
        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainRoadPlanEntity ReadBindTrainWorkInfo(IDataReader dataReader)
        {
            TrainRoadPlanEntity model = new TrainRoadPlanEntity();
            //主键
            model.TrainRoadPlanId = CommonDBCheck.ToLong(dataReader["train_road_plan_id"]);
            //计划时间
            model.Plandate = CommonDBCheck.ToDateTime(dataReader["plan_date"]);

            model.TrainRoadId = CommonDBCheck.ToInt(dataReader["train_road_id"]);

            return model;
        }
        #endregion



        #region 得到将要退乘的列车信息

        /// <summary>
        /// 得到将要退乘的列车信息
        /// </summary>
        /// <returns></returns>
        public List<TrainRoadPlanEntity> GetTrainRestInfo(int ntime)
        {
            List<TrainRoadPlanEntity> model = new List<TrainRoadPlanEntity>();
            try
            {
                string strSql = GetTrainRestInfoSql(ntime);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindTrainRestInfo(dataReader));
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 得到将要退乘的列车信息Sql
        /// </summary>
        /// <param name="traincode"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private string GetTrainRestInfoSql(int ntime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select train_road_plan_id ");
            strSql.Append(@",train_road_id");
            strSql.Append(@",run_date plan_date ");
            strSql.Append(@" from dbo.t_train_run ");
            strSql.Append(@" where status=1 ");
            strSql.Append(@" and DATEDIFF(n, end_time, getdate()) > " + ntime);
            return strSql.ToString();
        }
        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainRoadPlanEntity ReadBindTrainRestInfo(IDataReader dataReader)
        {
            TrainRoadPlanEntity model = new TrainRoadPlanEntity();
            //主键
            model.TrainRoadPlanId = CommonDBCheck.ToLong(dataReader["train_road_plan_id"]);
            //计划时间
            model.Plandate = CommonDBCheck.ToDateTime(dataReader["plan_date"]);

            model.TrainRoadId = CommonDBCheck.ToInt(dataReader["train_road_id"]);

            return model;
        }
        #endregion

   
        #region 出乘
        /// <summary>
        /// 出乘
        /// </summary>
        public void Onduty(DateTime plan_date, string trainroadid)
        {

            try
            {
                string strSql = OndutySql(plan_date, trainroadid);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 出乘
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string OndutySql(DateTime plan_date, string trainroadid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec dbo.p_create_run '" + plan_date + "'," + trainroadid);
            return strSql.ToString();
        }
        #endregion

        /// <summary>
        /// 更新列车的位置
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="position"></param>
        /// <param name="flag"></param>
        public void SetMachinePosition(MachineEntity machine, PositionEntity position, EnLocationFlag flag)
        {
            try
            {
                string strSql = SetMachinePositionSql(machine, position, flag);
#if DEBUG
                Log.WriteDebug(strSql);
#endif
                ExecuteNonQuery(strSql);
            }
            catch (Exception e)
            {
                Log.WriteError(e.Message);
                throw e;
            }
        }

        private string SetMachinePositionSql(MachineEntity machine, PositionEntity position, EnLocationFlag flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  t_train_location set");
            strSql.Append(" longitude = '" + position.Longitude.ToString() + "'");
            strSql.Append(",latitude = '" + position.Latitude.ToString() + "'");
            strSql.Append(",status = '" + ((int)position.Status).ToString() + "'");
            strSql.Append(",current_datetime = getdate()");
            strSql.Append(" where machine_id = '" + machine.Machine_id + "'");
            return strSql.ToString();
        }


        public DataTable GetStationByTrain(string strtrainid, DateTime plandate)
        {
            try
            {
                return ExecuteDataSet(GetStationByTrainSql(strtrainid, plandate)).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        private string GetStationByTrainSql(string strtrainroadid, DateTime plandate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" t_train_road_smallstation.train_road_code");
            strSql.Append(",t_train_road_smallstation.station");
            strSql.Append(",t_train_road_smallstation.station_id");
            strSql.Append(",t_train_road_smallstation.Longitude");
            strSql.Append(",t_train_road_smallstation.Latitude");
            strSql.Append(",t_train_road_smallstation.point_x");
            strSql.Append(",t_train_road_smallstation.point_y");
            strSql.Append(" from t_train_road_smallstation");
            strSql.Append(" where t_train_road_smallstation.train_road_code = '" + strtrainroadid + "'");
            // strSql.Append(" and v_train_road_station.Longitude != 0 and v_train_road_station.Longitude is not null");
            // strSql.Append(" and v_train_road_station.Latitude != 0 and v_train_road_station.Latitude is not null");

            strSql.Append(" and t_train_road_smallstation.point_x != 0 and t_train_road_smallstation.point_x is not null");
            strSql.Append(" and t_train_road_smallstation.point_y != 0 and t_train_road_smallstation.point_y is not null");
            strSql.Append(" and t_train_road_smallstation.train_road_date = '" + plandate + "'");
            strSql.Append(" order by t_train_road_smallstation.seq");
            return strSql.ToString();
        }


        public List<TrainRunTrainStationEntity> GetStationByTrains(string strtrainid, DateTime plandate)
        {
            try
            {
                List<TrainRunTrainStationEntity> model = new List<TrainRunTrainStationEntity>();
                try
                {
                    string strSql = GetStationByTrainsSql(strtrainid, plandate);
                    using (IDataReader dataReader = ExecuteReader(strSql))
                    {
                        while (dataReader.Read())
                        {
                            model.Add(ReadBindByTrains(dataReader));
                        }

                    }
                    return model;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        private string GetStationByTrainsSql(string strtrainroadid, DateTime plandate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" t_train_road_smallstation.train_road_code");
            strSql.Append(",t_train_road_smallstation.station");
            strSql.Append(",t_train_road_smallstation.station_id");
            strSql.Append(",t_train_road_smallstation.Longitude");
            strSql.Append(",t_train_road_smallstation.Latitude");
            strSql.Append(",t_train_road_smallstation.point_x");
            strSql.Append(",t_train_road_smallstation.point_y");
            strSql.Append(" from t_train_road_smallstation");
            strSql.Append(" where t_train_road_smallstation.train_road_code = '" + strtrainroadid + "'");
            //begin zyp
            //strSql.Append(" and v_train_road_station.Longitude != 0 and v_train_road_station.Longitude is not null");
            // strSql.Append(" and v_train_road_station.Latitude != 0 and v_train_road_station.Latitude is not null");
            strSql.Append(" and t_train_road_smallstation.point_x != 0 and t_train_road_smallstation.point_x is not null");
            strSql.Append(" and t_train_road_smallstation.point_y != 0 and t_train_road_smallstation.point_y is not null");
            //end zyp
            strSql.Append(" and t_train_road_smallstation.train_road_date = '" + plandate + "'");
            strSql.Append(" order by t_train_road_smallstation.seq");
            return strSql.ToString();
        }

        private TrainRunTrainStationEntity ReadBindByTrains(IDataReader dataReader)
        {
            TrainRunTrainStationEntity model = new TrainRunTrainStationEntity();
            model.Station.Name = CommonDBCheck.ToString(dataReader["station"]);
            model.Station.Id = CommonDBCheck.ToInt(dataReader["station_id"]);
            model.Station.Position.Longitude = CommonDBCheck.ToDouble(dataReader["Longitude"]);
            model.Station.Position.Latitude = CommonDBCheck.ToDouble(dataReader["Latitude"]);
            model.Station.Position.X = CommonDBCheck.ToInt(dataReader["point_x"]);
            model.Station.Position.Y = CommonDBCheck.ToInt(dataReader["point_y"]);
            return model;
        }


        #region 得到走过的车站

        /// <summary>
        /// 通过设备id得到列车信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TrainRunTrainStationEntity> GetInfoStaion(long trainruntrain)
        {
            List<TrainRunTrainStationEntity> model = new List<TrainRunTrainStationEntity>();
            try
            {
                string strSql = GetInfoStaionSql(trainruntrain);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    while (dataReader.Read())
                    {
                        model.Add(ReadBindTrainStation(dataReader));
                    }

                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 得到列车信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetInfoStaionSql(long trainruntrain)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" station_id");
            strSql.Append(",dbo.f_get_station_name(station_id) station_name");
            strSql.Append(",seq");
            strSql.Append(",arr_datetime");
            strSql.Append(",dep_datetime");
            strSql.Append(",arr_odatetime");
            strSql.Append(",dep_odatetime");
            strSql.Append(",dep_trainnum");
            strSql.Append(",arr_trainnum");
            strSql.Append(" from t_train_run_train_station");
            strSql.Append(" where train_run_train_id ='" + trainruntrain + "'");
            strSql.Append(" and (arr_odatetime<getdate() or dep_odatetime<getdate())");
            strSql.Append(" order by seq desc");
            return strSql.ToString();
        }

        /// <summary>
        /// 将数据添加到实体类中
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private TrainRunTrainStationEntity ReadBindTrainStation(IDataReader dataReader)
        {
            TrainRunTrainStationEntity pe = new TrainRunTrainStationEntity();
            pe.Station.Id = CommonDBCheck.ToInt(dataReader["station_id"]);
            pe.Station.Name = CommonDBCheck.ToString(dataReader["station_name"]);
            pe.Seq = CommonDBCheck.ToInt(dataReader["seq"]);
            pe.Arrdatetime = CommonDBCheck.ToDateTime(dataReader["arr_datetime"]);
            pe.Depdatetime = CommonDBCheck.ToDateTime(dataReader["dep_datetime"]);
            //pe.Arrdatetime = CommonDBCheck.ToDateTime(dataReader["arr_odatetime"]);
           // pe.Depdatetime = CommonDBCheck.ToDateTime(dataReader["dep_odatetime"]);
            pe.Dep_trainnum = CommonDBCheck.ToString(dataReader["dep_trainnum"]);
            pe.Arr_trainnum = CommonDBCheck.ToString(dataReader["arr_trainnum"]);
            return pe;
        }
        #endregion

        #region 更新出乘时间

        /// <summary>
        /// 更新出乘时间
        /// </summary>
        /// <param name="tie"></param>
        public void UpdateRunTime(long trainruntrainid)
        {
            try
            {
                string strSql = UpdateRunTimeSql(trainruntrainid);
                ExecuteNonQuery(strSql);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 更新出乘时间Sql
        /// </summary>
        /// <param name="tie"></param>
        /// <returns></returns>
        private string UpdateRunTimeSql(long trainruntrainid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec p_run_time_update '" + trainruntrainid + "'");

            return strSql.ToString();
        }

        #endregion
    }
}
