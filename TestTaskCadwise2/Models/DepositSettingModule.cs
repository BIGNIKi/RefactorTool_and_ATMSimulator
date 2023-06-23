using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestTaskCadwise2.Models
{
    public static class DepositSettingModule
    {
        public static bool CalculateCountOfBanknotesDeposit( int sum,
            ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo )
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
                SettingUpBtnDeposit(banknotesSelectorInfo);
                return true; // success
            }

            return false;

            // распределение некоторой суммы денег (sum) по количеству купюр (учитывая возможность вместить эти купюры в банкомат)
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
                    if(banknotesSelectorInfo[i].CountNowInATM + countOfBanknotes > banknotesSelectorInfo[i].Capacity)
                    {
                        countOfBanknotes = banknotesSelectorInfo[i].Capacity - banknotesSelectorInfo[i].CountNowInATM;
                    }
                    sum -= banknoteValue * countOfBanknotes;
                    banknotesSelectorInfo[i].Count = countOfBanknotes;
                }
            }
        }

        public static void ResetBtns( ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo )
        {
            foreach(var item in banknotesSelectorInfo)
            {
                item.IsMinusEnabled = false;
                item.IsPlusEnabled = false;
            }
        }

        public static void SettingUpBtnDeposit( ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo )
        {
            for(int i = banknotesSelectorInfo.Count - 1; i >= 0; i--)
            {
                SettingUpMinusBtns(banknotesSelectorInfo, i);

                SettingUpPlusBtns(banknotesSelectorInfo, i);
            }
        }

        private static void SettingUpMinusBtns( ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo, int btnsId )
        {
            if(banknotesSelectorInfo[btnsId].Count > 0)
            {
                int needDistribute = banknotesSelectorInfo[btnsId].BanknoteValue; // всего надо распределить денег
                var clickData = new Dictionary<int, int>();
                for(int j = btnsId - 1; j >= 0; j--)
                {
                    int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                    if(countOfBanknotes + banknotesSelectorInfo[j].Count + banknotesSelectorInfo[j].CountNowInATM <= banknotesSelectorInfo[j].Capacity)
                    {
                        clickData.Add(j, countOfBanknotes);
                        clickData.Add(btnsId, -1);
                        banknotesSelectorInfo[btnsId].IsMinusEnabled = true;
                        break;
                    }
                    else
                    {
                        // часть денег можно занять купюрой меньшего достоинства
                        needDistribute -= (banknotesSelectorInfo[j].Capacity - banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count) * banknotesSelectorInfo[j].BanknoteValue;
                        clickData.Add(j, banknotesSelectorInfo[j].Capacity - banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count);
                    }
                }

                if(!banknotesSelectorInfo[btnsId].IsMinusEnabled)
                {
                    clickData.Clear();
                    for(int j = btnsId + 1; j < banknotesSelectorInfo.Count; j++)
                    {
                        int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[btnsId].BanknoteValue;
                        // если достаточно купюр i и хватает места в j
                        if(countOfBanknotes <= banknotesSelectorInfo[btnsId].Count
                            && banknotesSelectorInfo[j].CountNowInATM + banknotesSelectorInfo[j].Count + 1 <= banknotesSelectorInfo[j].Capacity)
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

        private static void SettingUpPlusBtns( ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo, int btnsId )
        {
            var clickData = new Dictionary<int, int>();
            for(int j = btnsId + 1; j < banknotesSelectorInfo.Count; j++)
            {
                if(banknotesSelectorInfo[j].Count > 0)
                {
                    int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[btnsId].BanknoteValue;
                    if(countOfBanknotes + banknotesSelectorInfo[btnsId].Count + banknotesSelectorInfo[btnsId].CountNowInATM <= banknotesSelectorInfo[btnsId].Capacity)
                    {
                        clickData.Add(j, -1);
                        clickData.Add(btnsId, countOfBanknotes);
                        banknotesSelectorInfo[btnsId].IsPlusEnabled = true;
                    }
                    break; // если не хватило места для купюры номинала j, то для j+1 точно не хватит места
                }
            }

            // если можно добавить одну купюру в i
            if(!banknotesSelectorInfo[btnsId].IsPlusEnabled
                && banknotesSelectorInfo[btnsId].CountNowInATM + banknotesSelectorInfo[btnsId].Count + 1 <= banknotesSelectorInfo[btnsId].Capacity)
            {
                clickData.Clear();
                int needDistribute = banknotesSelectorInfo[btnsId].BanknoteValue;
                for(int j = btnsId - 1; j >= 0; j--)
                {
                    int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                    if(banknotesSelectorInfo[j].Count >= countOfBanknotes)
                    {
                        banknotesSelectorInfo[btnsId].IsPlusEnabled = true;
                        clickData.Add(j, -countOfBanknotes);
                        clickData.Add(btnsId, 1);

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

        public static void ClickedBtn( ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo, int btnsId )
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
            ResetBtns(banknotesSelectorInfo);
            SettingUpBtnDeposit(banknotesSelectorInfo);
        }
    }
}
