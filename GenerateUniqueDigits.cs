namespace AdministratorSystem
{
    public string GenerateUniqueDigits()
    {
        Random rand = new Random();
        int randomNumber = rand.Next(100000, 999999); // Generates a random 6-digit number
        return randomNumber.ToString();
    }
}