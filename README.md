# Репозиторий содержит проект Visual Studio 2022 с 2-мя PET проектами

В данном репозитории представлено решение Visual Studio 2022 с 4 проектами.

#### 1) RefactorTool
C# WPF MVVM приложение для рефактора файлов.
Позволяет:
* Выбрать файл;
* Задать длину слов (все слова меньше этой длины будут удалены из файла);
* Вкл/откл удаление всех знаков препинания из файла;
* Выбрать место для сохранения итогового файла.

Задача на рефактор производится в background'е, **не препятствуя** дальнейшей работе с интерфейсом приложения. \
Каждая задача на рефактор **ставится в очередь**, о чем сингализирует надпись в нижнем левом углу главного меню. \
Все задачи на рефактор выполняются **последовательно**.

#### 2) RefactorTool.Tests
Содержит модульные тесты для проекта TestTaskCadwise1.

#### 3) ATMSimulator
C# WPF MVVM приложение банкомат.
Позволяет:
* отслеживать баланс пользователя и наличие купюр различного номинала;
* снимать наличные (с настройкой выдачи купюр);
* вносить наличные (также можно настроить купюры, которые поданы на вход);
* изменять язык приложения (en, rus);
* банкомат имеет ограничение на количество купюр.

#### 4) ATMSimulator.Tests
Содержит модульные тесты для проекта TestTaskCadwise2.
