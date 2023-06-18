using System;
using System.Collections.ObjectModel;

namespace TestTaskCadwise2.Models
{
    public static class MathModule
    {
        public static bool CalculateCountOfBanknotes( int sum,
            ObservableCollection<DepositeBanknoteInfo> banknotesSelectorInfo )
        {
            foreach(var item in banknotesSelectorInfo)
            {
                item.Count = 0;
                item.IsMinusEnabled = false;
                item.IsPlusEnabled = false;
            }

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

            if(sum == 0)
            {
                SettingUpBtnDeposit(banknotesSelectorInfo);
                return true; // success
            }

            return false;
        }

        private static void ResetBtns( ObservableCollection<DepositeBanknoteInfo> banknotesSelectorInfo )
        {
            foreach(var item in banknotesSelectorInfo)
            {
                item.IsMinusEnabled = false;
                item.IsPlusEnabled = false;
            }
        }

        public static void SettingUpBtnDeposit( ObservableCollection<DepositeBanknoteInfo> banknotesSelectorInfo )
        {
            for(int i = banknotesSelectorInfo.Count - 1; i >= 0; i--)
            {
                if(banknotesSelectorInfo[i].Count > 0)
                {
                    int needDistribute = banknotesSelectorInfo[i].BanknoteValue; // всего надо распределить денег
                    for(int j = i - 1; j >= 0; j--)
                    {
                        int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                        if(countOfBanknotes + banknotesSelectorInfo[j].Count + banknotesSelectorInfo[j].CountNowInATM <= banknotesSelectorInfo[j].Capacity)
                        {
                            banknotesSelectorInfo[i].IsMinusEnabled = true;
                            break;
                        }
                        else
                        {
                            // часть денег можно занять купюрой меньшего достоинства
                            needDistribute -= (banknotesSelectorInfo[j].Capacity - banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count) * banknotesSelectorInfo[j].BanknoteValue;
                        }
                    }

                    if(!banknotesSelectorInfo[i].IsMinusEnabled)
                    {
                        for(int j = i + 1; j < banknotesSelectorInfo.Count; j++)
                        {
                            int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[i].BanknoteValue;
                            // если достаточно купюр i и хватает места в j
                            if(countOfBanknotes <= banknotesSelectorInfo[i].Count
                                && banknotesSelectorInfo[j].CountNowInATM + banknotesSelectorInfo[j].Count + 1 <= banknotesSelectorInfo[j].Capacity)
                            {
                                banknotesSelectorInfo[i].IsMinusEnabled = true;
                                break;
                            }
                        }
                    }
                }

                for(int j = i + 1; j < banknotesSelectorInfo.Count; j++)
                {
                    if(banknotesSelectorInfo[j].Count > 0)
                    {
                        int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[i].BanknoteValue;
                        if(countOfBanknotes + banknotesSelectorInfo[i].Count + banknotesSelectorInfo[i].CountNowInATM <= banknotesSelectorInfo[i].Capacity)
                        {
                            banknotesSelectorInfo[i].IsPlusEnabled = true;
                        }
                        break; // если не хватило места для купюры номинала j, то для j+1 точно не хватит места
                    }
                }

                // если можно добавить одну купюру в i
                if(!banknotesSelectorInfo[i].IsPlusEnabled
                    && banknotesSelectorInfo[i].CountNowInATM + banknotesSelectorInfo[i].Count + 1 <= banknotesSelectorInfo[i].Capacity)
                {
                    int needDistribute = banknotesSelectorInfo[i].BanknoteValue;
                    for(int j = i - 1; j >= 0; j--)
                    {
                        int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                        if(banknotesSelectorInfo[j].Count >= countOfBanknotes)
                        {
                            banknotesSelectorInfo[i].IsPlusEnabled = true;
                            break;
                        }
                        else
                        {
                            needDistribute -= banknotesSelectorInfo[j].BanknoteValue * banknotesSelectorInfo[j].Count;
                        }
                    }
                }
            }
        }

        public static void SettingUpBanknoteCount( ObservableCollection<DepositeBanknoteInfo> banknotesSelectorInfo, int orderBtnId)
        {
            if(orderBtnId < 0) // pressed minus
            {
                orderBtnId++;
                orderBtnId = Math.Abs(orderBtnId);
                int needDistribute = banknotesSelectorInfo[orderBtnId].BanknoteValue; // всего надо распределить денег
                bool isMinusDone = false;
                for(int j = orderBtnId - 1; j >= 0; j--)
                {
                    int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                    if(countOfBanknotes + banknotesSelectorInfo[j].Count + banknotesSelectorInfo[j].CountNowInATM <= banknotesSelectorInfo[j].Capacity)
                    {
                        banknotesSelectorInfo[j].Count += countOfBanknotes;
                        banknotesSelectorInfo[orderBtnId].Count--;
                        banknotesSelectorInfo[orderBtnId].IsMinusEnabled = true;
                        isMinusDone = true;
                        break;
                    }
                    else
                    {
                        // часть денег можно занять купюрой меньшего достоинства
                        needDistribute -= (banknotesSelectorInfo[j].Capacity - banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count) * banknotesSelectorInfo[j].BanknoteValue;
                        banknotesSelectorInfo[j].Count += banknotesSelectorInfo[j].Capacity - banknotesSelectorInfo[j].CountNowInATM - banknotesSelectorInfo[j].Count;
                    }
                }
                if(!isMinusDone)
                {
                    for(int j = orderBtnId + 1; j < banknotesSelectorInfo.Count; j++)
                    {
                        int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[orderBtnId].BanknoteValue;
                        // если достаточно купюр i и хватает места в j
                        if(countOfBanknotes <= banknotesSelectorInfo[orderBtnId].Count
                            && banknotesSelectorInfo[j].CountNowInATM + banknotesSelectorInfo[j].Count + 1 <= banknotesSelectorInfo[j].Capacity)
                        {
                            banknotesSelectorInfo[orderBtnId].Count -= countOfBanknotes;
                            banknotesSelectorInfo[j].Count++;
                            break;
                        }
                    }
                }
            }
            else
            {
                orderBtnId--;
                bool isPlusDone = false;
                for(int j = orderBtnId + 1; j < banknotesSelectorInfo.Count; j++)
                {
                    if(banknotesSelectorInfo[j].Count > 0)
                    {
                        int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[orderBtnId].BanknoteValue;
                        if(countOfBanknotes + banknotesSelectorInfo[orderBtnId].Count + banknotesSelectorInfo[orderBtnId].CountNowInATM <= banknotesSelectorInfo[orderBtnId].Capacity)
                        {
                            banknotesSelectorInfo[j].Count--;
                            banknotesSelectorInfo[orderBtnId].Count += countOfBanknotes;
                            isPlusDone = true;
                        }
                        break; // если не хватило места для купюры номинала j, то для j+1 точно не хватит места
                    }
                }

                if(!isPlusDone
                    && banknotesSelectorInfo[orderBtnId].CountNowInATM + banknotesSelectorInfo[orderBtnId].Count + 1 <= banknotesSelectorInfo[orderBtnId].Capacity)
                {
                    int needDistribute = banknotesSelectorInfo[orderBtnId].BanknoteValue;
                    for(int j = orderBtnId - 1; j >= 0; j--)
                    {
                        int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                        if(banknotesSelectorInfo[j].Count >= countOfBanknotes)
                        {
                            banknotesSelectorInfo[j].Count -= countOfBanknotes;
                            banknotesSelectorInfo[orderBtnId].Count++;
                            break;
                        }
                        else
                        {
                            needDistribute -= banknotesSelectorInfo[j].BanknoteValue * banknotesSelectorInfo[j].Count;
                            banknotesSelectorInfo[j].Count = 0;
                        }
                    }
                }
            }

            ResetBtns(banknotesSelectorInfo);
            SettingUpBtnDeposit(banknotesSelectorInfo);
        }
    }
}
