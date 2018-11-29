

Public Class Application
    Public Shared Sub Main()
        Dim squares(8) As String
          
        'Set the initial values of the squares
        For i As Integer = 0 To 8
            squares(i) = " "
        Next
        
        'The main game loop
        While BoardIsFull(squares) = False
        
            System.Console.Clear()
            
            'Write the Board
            WriteBoard(squares)
            
            System.Console.WriteLine("You are 'X', and the Computer is 'O'.")
            System.Console.WriteLine("Please enter the number of the square you would like to play on:")
            
            'Use Try/Catch to validate user input as an integer
            Try
                Dim playedSquare As Integer = Integer.Parse(System.Console.ReadLine()) - 1
                
                If SquareIsOpen(squares(playedSquare)) Then
                    squares(playedSquare) = "X"
                Else
                    System.Console.WriteLine("Invalid Square!")
                End If
            
            System.Console.WriteLine($"You played on square {playedSquare}")
            Catch
                System.Console.WriteLine("Invalid square - you must enter a number between 1 and 9!")
                System.Console.WriteLine("Press Enter to try again >>")
                System.Console.ReadLine()
            End Try 
            
            
            
        End While
        
        'Write the full board one last time
        WriteBoard(squares)
        System.Console.Writeline("Game Over! The Board was completely filled up!")
        
    End Sub
    
    Public Shared Sub WriteBoard(squares As String())
        Dim line As String = "-----"
        
        'Write the board to the console'
        System.Console.WriteLine($"{squares(0)}|{squares(1)}|{squares(2)}")
        System.Console.WriteLine($"{line}")
        System.Console.WriteLine($"{squares(3)}|{squares(4)}|{squares(5)}")
        System.Console.WriteLine($"{line}")
        System.Console.WriteLine($"{squares(6)}|{squares(7)}|{squares(8)}")
    End Sub
    
    Public Shared Function SquareIsOpen(square As String) As Boolean
        Return If (square = " ", True, False)
    End Function
    
    Public Shared Function BoardIsFull(squares As String()) As Boolean
        For Each square As String In squares
            If square = " " Then
                Return False
            End If
        Next
        
        Return True
    End Function
End Class
