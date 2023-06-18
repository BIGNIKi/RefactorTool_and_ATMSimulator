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

        public static void SettingUpBtnDeposit( ObservableCollection<DepositeBanknoteInfo> banknotesSelectorInfo )
        {
            for(int i = banknotesSelectorInfo.Count - 1; i >= 0; i--)
            {
                if(banknotesSelectorInfo[i].Count > 0)
                {
                    int needDistribute = banknotesSelectorInfo[i].Count * banknotesSelectorInfo[i].BanknoteValue; // всего надо распределить денег
                    for(int j = i - 1; j >= 0; j--)
                    {
                        int countOfBanknotes = needDistribute / banknotesSelectorInfo[j].BanknoteValue;
                        if(countOfBanknotes + banknotesSelectorInfo[j].CountNowInATM <= banknotesSelectorInfo[j].Capacity)
                        {
                            banknotesSelectorInfo[i].IsMinusEnabled = true;
                            break;
                        }
                        else
                        {
                            // часть денег можно занять купюрой меньшего достоинства
                            needDistribute -= (banknotesSelectorInfo[j].Capacity - banknotesSelectorInfo[j].CountNowInATM) * banknotesSelectorInfo[j].BanknoteValue;
                        }
                    }

                    if(!banknotesSelectorInfo[i].IsMinusEnabled)
                    {
                        for(int j = i + 1; j < banknotesSelectorInfo.Count; j++)
                        {
                            int countOfBanknotes = banknotesSelectorInfo[j].BanknoteValue / banknotesSelectorInfo[i].BanknoteValue;
                            // если достаточно купюр i и хватает места в j
                            if(countOfBanknotes <= banknotesSelectorInfo[i].Count && banknotesSelectorInfo[j].Count < banknotesSelectorInfo[j].Capacity)
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
                        if(countOfBanknotes + banknotesSelectorInfo[i].Count <= banknotesSelectorInfo[i].Capacity)
                        {
                            banknotesSelectorInfo[i].IsPlusEnabled = true;
                        }
                        break; // если не хватило места для купюры номинала j, то для j+1 точно не хватит места
                    }
                }

                // если можно добавить одну купюру в i
                if(!banknotesSelectorInfo[i].IsPlusEnabled && banknotesSelectorInfo[i].Count < banknotesSelectorInfo[i].Capacity)
                {
                    int needDistribute = banknotesSelectorInfo[i].BanknoteValue;
                    for(int j = i - 1; j >= 0; j--)
                    {
                        int countOfBanknotes = banknotesSelectorInfo[i].BanknoteValue / banknotesSelectorInfo[j].BanknoteValue;
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
    }
}
