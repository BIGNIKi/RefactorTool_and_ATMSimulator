using System;
using System.Collections.Generic;

namespace TestTaskCadwise2.Models
{
    public static class CashWithdrawalSettingModule
    {
        // расчитает возможно ли забрать деньги и если можно, то какими купюрами
        public static bool CalculateCountOfBanknotesCashWithdrawal( int sum,
            IList<SettingBanknoteInfo> banknotesSelectorInfo )
        {
            foreach(var item in banknotesSelectorInfo)
            {
                item.Count = 0;
                item.IsMinusEnabled = false;
                item.IsPlusEnabled = false;
            }

            DistributeSumToBanknotesIfPossible();

            if(sum == 0)
            {
                SettingUpBtnCashWithdrawal(banknotesSelectorInfo);
                return true; // success
            }

            return false;

            // распределение некоторой суммы денег (sum) по количеству купюр (учитывая наличие этих самых купюр в банкомате)
            void DistributeSumToBanknotesIfPossible()
            {
                for(int i = banknotesSelectorInfo.Count - 1; i >= 0; i--)
                {
                    int banknoteValue = banknotesSelectorInfo[i].BanknoteValue;
                    if(sum < banknoteValue)
                    {
                        continue;
                    }

                    int countOfBanknotes = sum / banknoteValue;
                    if(banknotesSelectorInfo[i].CountNowInATM - countOfBanknotes < 0)
                    {
                        countOfBanknotes = banknotesSelectorInfo[i].CountNowInATM;
                    }
                    sum -= banknoteValue * countOfBanknotes;
                    banknotesSelectorInfo[i].Count = countOfBanknotes;
                }
            }
        }

        /// <summary>
        /// Настройка состояния кнопок (вкл/выкл) с учетом возможности конвертации одних, уже выбранных, купюр в другие и наличия допольнительных купюр в банкомате
        /// </summary>
        /// <param name="banknotesSelectorInfo"></param>
        public static void SettingUpBtnCashWithdrawal( IList<SettingBanknoteInfo> banknotesSelectorInfo )
        {
            for(int i = banknotesSelectorInfo.Count - 1; i >= 0; i--)
            {
                SettingUpMinusBtns(banknotesSelectorInfo, i);

                SettingUpPlusBtns(banknotesSelectorInfo, i);
            }
        }

        private static void SettingUpMinusBtns( IList<SettingBanknoteInfo> banknotesSelectorInfo, int btnsId )
        {
            if(banknotesSelectorInfo[btnsId].Count > 0)
            {
                int needDistribute = banknotesSelectorInfo[btnsId].BanknoteValue;
                var clickData = new Dictionary<int, int>();
                clickData.Add(btnsId, -1);
                for(int j = btnsId - 1; j >= 0; j--)
                {
                    int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                    if(banknotesSelectorInfo[j].CountNowInATM - (countOfBanknotes + banknotesSelectorInfo[j].Count) >= 0)
                    {
                        clickData.Add(j, countOfBanknotes);

                        banknotesSelectorInfo[btnsId].IsMinusEnabled = true;
                        break;
                    }
                    else
                    {
                        needDistribute -= (banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count) * banknotesSelectorInfo[j].BanknoteValue;
                        clickData.Add(j, banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count);
                    }
                }

                if(!banknotesSelectorInfo[btnsId].IsMinusEnabled)
                {
                    clickData.Clear();
                    for(int j = btnsId + 1; j < banknotesSelectorInfo.Count; j++)
                    {
                        int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[btnsId].BanknoteValue;
                        if(countOfBanknotes <= banknotesSelectorInfo[btnsId].Count
                            && banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count - 1 >= 0)
                        {
                            banknotesSelectorInfo[btnsId].IsMinusEnabled = true;
                            clickData.Add(btnsId, -countOfBanknotes);
                            clickData.Add(j, 1);
                            break;
                        }
                    }
                }

                if(banknotesSelectorInfo[btnsId].IsMinusEnabled)
                {
                    banknotesSelectorInfo[btnsId].MinusClickedData = clickData;
                }
            }
        }

        private static void SettingUpPlusBtns( IList<SettingBanknoteInfo> banknotesSelectorInfo, int btnsId )
        {
            var clickData = new Dictionary<int, int>();
            clickData.Add(btnsId, 0);
            for(int j = btnsId + 1; j < banknotesSelectorInfo.Count; j++)
            {
                if(banknotesSelectorInfo[j].Count > 0)
                {
                    int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[btnsId].BanknoteValue;
                    if(banknotesSelectorInfo[btnsId].CountNowInATM - (countOfBanknotes + banknotesSelectorInfo[btnsId].Count) >= 0)
                    {
                        clickData.Add(j, -1);
                        clickData[btnsId] += countOfBanknotes;
                        banknotesSelectorInfo[btnsId].IsPlusEnabled = true;
                    }
                    break;
                }
            }

            if(!banknotesSelectorInfo[btnsId].IsPlusEnabled
                && banknotesSelectorInfo[btnsId].CountNowInATM - banknotesSelectorInfo[btnsId].Count - 1 >= 0)
            {
                clickData.Clear();
                clickData.Add(btnsId, 0);
                int needDistribute = banknotesSelectorInfo[btnsId].BanknoteValue;
                for(int j = btnsId - 1; j >= 0; j--)
                {
                    int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                    if(banknotesSelectorInfo[j].Count >= countOfBanknotes)
                    {
                        clickData.Add(j, -countOfBanknotes);
                        clickData[btnsId]++;
                        banknotesSelectorInfo[btnsId].IsPlusEnabled = true;
                        break;
                    }
                    else
                    {
                        needDistribute -= banknotesSelectorInfo[j].BanknoteValue * banknotesSelectorInfo[j].Count;
                        clickData.Add(j, -banknotesSelectorInfo[j].Count);
                    }
                }
            }

            if(banknotesSelectorInfo[btnsId].IsPlusEnabled)
            {
                banknotesSelectorInfo[btnsId].PlusClickedData = clickData;
            }
        }

        public static void ClickedBtn( IList<SettingBanknoteInfo> banknotesSelectorInfo, int btnsId )
        {
            if(btnsId < 0) // pressed minus
            {
                btnsId++;
                btnsId = Math.Abs(btnsId);
                foreach(var item in banknotesSelectorInfo[btnsId].MinusClickedData)
                {
                    banknotesSelectorInfo[item.Key].Count += item.Value;
                }
            }
            else
            {
                btnsId--;
                foreach(var item in banknotesSelectorInfo[btnsId].PlusClickedData)
                {
                    banknotesSelectorInfo[item.Key].Count += item.Value;
                }
            }
            DepositSettingModule.ResetBtns(banknotesSelectorInfo);
            SettingUpBtnCashWithdrawal(banknotesSelectorInfo);
        }
    }
}
