using System;
using System.Data.SQLite;  // Необходимо добавить пакет System.Data.SQLite

class Program
{
    static void Main(string[] args)
    {
        // Создание или подключение к базе данных SQLite
        string connectionString = "Data Source=example.db;Version=3;";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            CreateTable(connection);
            InsertStudent(connection, "Иван", "Ненов", "Математика", 20);
            InsertStudent(connection, "Егор", "Ржечковский", "Математика", 21);
            InsertTeacher(connection, "Ирина", "Вениаминовна", "Математика");
            InsertCourse(connection, "Математика", "Дискретная математика", 1);
            InsertExam(connection, "01.01.2025", 1);
            InsertGrade(connection, 1, 1, 5);
            InsertGrade(connection, 2, 1, 5);
            string want;
            int stop = 0;
            Console.WriteLine("Здравствуйте, что хотите сделать?");
            while (stop == 0)
            {
                try
                {
                    Console.WriteLine("1.1 - добавить студента, 1.2 - добавить учителя, 1.3 - добавить курс, 1.4 - добавить экзамен, 1.5 - добавить оценку студенту\n" +
                        "2.1 - изменить информацию о студенте, 2.2 - изменить информацию о преподавателях, 2.3 - изменить информацию о курсах\n" +
                        "3.1 - удалить студента, 3.2 - удалить преподавателя, 3.3 - удалить курс, 3.4 - удалить экзамен\n" +
                        "4 - получить список студентов по курсу\n" +
                        "5 - получить список курсов,читаемых данным преподавателем\n" +
                        "6 - получить список студентов данного курса\n" +
                        "7 - получить список оценок по данному курсу\n" +
                        "8 - средний бал данного студента по данному курсу\n" +
                        "9 - средний балл данного студента\n" +
                        "10 - средний бал по данному факультету\n" +
                        "stop - завершить работу\n");
                    want = Console.ReadLine();
                    if (want == "1.1") { Console.WriteLine("Введите имя, фамилию, факультет и возраст студента"); InsertStudent(connection, Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), int.Parse(Console.ReadLine())); }
                    else if (want == "1.2") { Console.WriteLine("Введите имя, фамилию и факультет учителя"); InsertTeacher(connection, Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); }
                    else if (want == "1.3") { Console.WriteLine("Введите название, описание и ID учителя для курса"); InsertCourse(connection, Console.ReadLine(), Console.ReadLine(), int.Parse(Console.ReadLine())); }
                    else if (want == "1.4") { Console.WriteLine("Введите дату и ID курса для экзамена"); InsertExam(connection, Console.ReadLine(), int.Parse(Console.ReadLine())); }
                    else if (want == "1.5") { Console.WriteLine("Введите ID студента, ID экзамена и саму оценку"); InsertGrade(connection, int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())); }
                    else if (want == "2.1") { Console.WriteLine("Введите ID студента, которого надо поменять, а затем имя, фамилию, факультет и возраст для их обновления"); UpdateStudent(connection, int.Parse(Console.ReadLine()), Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), int.Parse(Console.ReadLine())); }
                    else if (want == "2.2") { Console.WriteLine("Введите ID учителя, которого надо поменять, а затем имя, фамилию, факультет для их обновления"); UpdateTeacher(connection, int.Parse(Console.ReadLine()), Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); }
                    else if (want == "2.3") { Console.WriteLine("Введите ID курса, который надо поменять, а затем название, описание и ID учителя для их обновления"); UpdateCourse(connection, int.Parse(Console.ReadLine()), Console.ReadLine(), Console.ReadLine(), int.Parse(Console.ReadLine())); }
                    else if (want == "3.1") { Console.WriteLine("Введите ID студента для удаления"); DeleteStudent(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "3.2") { Console.WriteLine("Введите ID учителя для удаления"); DeleteTeacher(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "3.3") { Console.WriteLine("Введите ID курса для удаления"); DeleteCourse(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "3.4") { Console.WriteLine("Введите ID экзамена для удаления"); DeleteExam(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "4") { Console.WriteLine("Введите название факультета, студентов которого хотите узнать"); PrintStudentsByDepartment(connection, Console.ReadLine()); }
                    else if (want == "5") { Console.WriteLine("Введите ID учителя, курсы которого хотите узнать"); PrintCoursesByTeacher(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "6") { Console.WriteLine("Введите ID курса, студентов которого хотите узнать"); PrintStudentsByCourse(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "7") { Console.WriteLine("Введите ID курса, оценки которого хотите узнать");  GradesByCourse(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "8") { Console.WriteLine("Введите ID студента и ID курса, по которому хотите узнать оценки этого студента"); AverageScoreByCourse(connection, int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())); }
                    else if (want == "9") { Console.WriteLine("Введите ID студента, оценки которого хотите узнать"); AverageScoreByStudent(connection, int.Parse(Console.ReadLine())); }
                    else if (want == "10") { Console.WriteLine("Введите название факультета, студентов которого хотите узнать"); AverageScoreByDepartment(connection, Console.ReadLine()); }
                    else if (want == "stop")
                    {
                        connection.Close();
                        Console.WriteLine("Соединение с базой данных закрыто, Adios");
                        stop = 1;
                    }

                    else if (want == "admin.1") { GetStudents(connection); }
                    else if (want == "admin.2") { GetTeachers(connection); }
                    else if (want == "admin.3") { GetCourses(connection); }
                    else if (want == "admin.4") { GetExams(connection); }
                    else if (want == "admin.5") { GetGrades(connection); }
                    else { Console.WriteLine("Вы неправильно ввели число, попробуйте повторить попытку..."); }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Что-то пошло не так, повторите Ваш запрос");
                }
            }
        }
    }

    static void CreateTable(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = @"CREATE TABLE IF NOT EXISTS Students (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT,
            surname TEXT,
            department TEXT,
            age INTEGER
            );

            CREATE TABLE IF NOT EXISTS Grades (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                student_id INTEGER,
                exam_id INTEGER,
                grade INTEGER,
                CONSTRAINT fk_student_id FOREIGN KEY (student_id) REFERENCES Students (id) ON DELETE SET NULL ON UPDATE CASCADE,
                CONSTRAINT fk_exam_id FOREIGN KEY (exam_id) REFERENCES Exams (id) ON DELETE SET NULL ON UPDATE CASCADE
            );

            CREATE TABLE IF NOT EXISTS Teachers (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT,
                surname TEXT,
                department TEXT
            );

            CREATE TABLE IF NOT EXISTS Courses (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT,
                description TEXT,
                teacher_id INTEGER,
                CONSTRAINT fk_teacher_id FOREIGN KEY (teacher_id) REFERENCES Teachers (id) ON DELETE SET NULL ON UPDATE CASCADE
            );

            CREATE TABLE IF NOT EXISTS Exams (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                session_date TEXT,
                course_id INTEGER,
                CONSTRAINT fk_course_id FOREIGN KEY (course_id) REFERENCES Courses (id) ON DELETE SET NULL ON UPDATE CASCADE
            );";
            command.ExecuteNonQuery();
            Console.WriteLine("Таблицы успешно созданы.");
        }
    }
    static void GradesByCourse(SQLiteConnection connection, int id)
    {;
        using (var command = new SQLiteCommand($"SELECT * FROM Grades g JOIN Exams e ON g.exam_id = e.id WHERE e.course_id = '{id}'", connection))
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine($"Оценки курса c ID {id}:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["grade"]}\n");
            }
        }
    }
    static void AverageScoreByDepartment(SQLiteConnection connection, string department)
    {
        using (var command = new SQLiteCommand($"SELECT AVG(grade) FROM Grades WHERE student_id in (SELECT id FROM Students WHERE department = '{department}')", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Cредний балл по факультету {department} - {reader["AVG(grade)"]}\n");

            }

        }
    }
    static void AverageScoreByStudent(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand($"SELECT *, AVG(g.grade) avg FROM Students s JOIN Grades g ON s.id = g.student_id WHERE s.id = '{id}'", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Cредний балл студента c ID {id} - {reader["avg"]}\n");

            }

        }
    }
    static void AverageScoreByCourse(SQLiteConnection connection, int student_id, int course_id)
    {
        using (var command = new SQLiteCommand($"SELECT *, AVG(g.grade) avg FROM Grades g JOIN Exams ex ON g.exam_id = ex.id JOIN Students s ON g.student_id = s.id WHERE ex.course_id = '{course_id}' AND s.id = '{student_id}'", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Средняя оценка студента {reader["name"]} {reader["surname"]}, учащегося на курсе {course_id} - {reader["avg"]}\n");
            }
        }
    }
    static void PrintStudentsByCourse(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand($"SELECT DISTINCT Students.* FROM Grades JOIN Students ON Grades.student_id == Students.id WHERE exam_id IN (SELECT DISTINCT id FROM Exams WHERE course_id = {id})", connection))
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine($"Студенты курса {id}:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["name"]} {reader["surname"]}\n");
            }
        }
    }
    static void PrintCoursesByTeacher(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand($"SELECT * FROM Courses WHERE teacher_id = '{id}'", connection))
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine($"Курсы, читаемые учителем с ID {id}:");
            while (reader.Read())
            {
                Console.WriteLine($"ID курса: {reader["id"]}\n");
            }
        }
    }
    static void PrintStudentsByDepartment(SQLiteConnection connection, string department)
    {
        using (var command = new SQLiteCommand($"SELECT * FROM Students WHERE department = '{department}'", connection))
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine($"Студенты курса {department}:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["name"]} {reader["surname"]}\n");
            }
        }
    }
    static void DeleteExam(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Exams WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Экзамен c ID {id} удален\n");
        }
    }
    static void DeleteCourse(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Courses WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Курс c ID {id} удален\n");
        }
    }
    static void DeleteTeacher(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Teachers WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Учитель с ID {id} удален\n");
        }
    }
    static void DeleteStudent(SQLiteConnection connection, int id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Students WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Студент с ID {id} удален\n");
        }
    }
    static void InsertGrade(SQLiteConnection connection, int student_id, int exam_id, int grade)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Grades (student_id, exam_id, grade) VALUES (@student_id, @exam_id, @grade)";
            command.Parameters.AddWithValue("@student_id", student_id);
            command.Parameters.AddWithValue("@exam_id", exam_id);
            command.Parameters.AddWithValue("@grade", grade);
            command.ExecuteNonQuery();
            Console.WriteLine($"Оценка {grade} добавлена студенту с ID: {student_id}\n");
        }
    }
    static void InsertStudent(SQLiteConnection connection, string name, string surname, string department, int age)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Students (name, surname, department, age) VALUES (@name, @surname, @department, @age)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@age", age);
            command.ExecuteNonQuery();
            Console.WriteLine($"Студент {name} {surname} добавлен\n");
        }
    }
    static void InsertTeacher(SQLiteConnection connection, string name, string surname, string department)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Teachers (name, surname, department) VALUES (@name, @surname, @department)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@department", department);
            command.ExecuteNonQuery();
            Console.WriteLine($"Преподаватель {name} {surname} добавлен\n");
        }
    }
    static void InsertCourse(SQLiteConnection connection, string title, string description, int teacher_id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Courses (title, description, teacher_id) VALUES (@title, @description, @teacher_id)";
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@teacher_id", teacher_id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Курс {title} добавлен\n");
        }
    }
    static void InsertExam(SQLiteConnection connection, string session_date, int course_id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Exams (session_date, course_id) VALUES (@session_date, @course_id)";
            command.Parameters.AddWithValue("@session_date", session_date);
            command.Parameters.AddWithValue("@course_id", course_id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Данный экзамен добавлен\n");
        }
    }
    static void GetGrades(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand("SELECT * FROM Grades", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, ID студента: {reader["student_id"]}, ID экзамена: {reader["exam_id"]}, Оценка: {reader["grade"]}\n");
            }
        }
    }
    static void GetStudents(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand("SELECT * FROM Students", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Имя и фамилия: {reader["name"]} {reader["surname"]}, Возраст: {reader["age"]}, Факультет: {reader["department"]}\n");
            }
        }
    }
    static void GetCourses(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand("SELECT * FROM Courses", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Название курса: {reader["title"]}, Описание курса: {reader["description"]}, ID учителя: {reader["teacher_id"]}\n");
            }
        }
    }
    static void GetTeachers(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand("SELECT * FROM Teachers", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Имя: {reader["name"]}, Фамилия: {reader["surname"]}, Курс: {reader["department"]}\n");
            }
        }
    }
    static void GetExams(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand("SELECT * FROM Exams", connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Дата: {reader["session_date"]}, Курс: {reader["course_id"]}\n");
            }
        }
    }
    static void UpdateStudent(SQLiteConnection connection, int id, string name, string surname, string department, int age)
    {
        Console.WriteLine("Введите ID студента, которого надо поменять, а затем имя, фамилию, факультет и возраст для их обновления");
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = $"UPDATE Students SET name = @name, surname = @surname, department = @department, age = @age WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@age", age);
            command.ExecuteNonQuery();
            Console.WriteLine($"Данные о студенте с ID {id} изменены\n");
        }
    }
    static void UpdateTeacher(SQLiteConnection connection, int id, string name, string surname, string department)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = $"UPDATE Students SET name = @name, surname = @surname, department = @department WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@department", department);
            command.ExecuteNonQuery();
            Console.WriteLine($"Данные об учителе с ID {id} изменены\n");
        }
    }
    static void UpdateCourse(SQLiteConnection connection, int id, string title, string description, int teacher_id)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = $"UPDATE Courses SET title = @title, description = @description, teacher_id = @teacher_id WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@teacher_id", teacher_id);
            command.ExecuteNonQuery();
            Console.WriteLine($"Данные о курсе с ID {id} изменены\n");
        }
    }   

}
