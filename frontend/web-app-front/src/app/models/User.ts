//align the User type with the server
export interface User {
    _id: string;
    jwt: string;
    firstName: string;
    lastName: string;
    dob: string;
    email: string;
    password: string;
    message: string;
}
  