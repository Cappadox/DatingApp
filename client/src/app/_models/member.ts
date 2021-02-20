import { Photo } from './photo';

export interface Member {
  id: number;
  userName: string;
  photoUrl: string;
  dateOfBirth: Date;
  knownAs: string;
  created: Date;
  lastActive: Date;
  gender: string;
  introduction: string;
  lookingFor: string;
  interests: string;
  country: string;
  city: string;
  photos: Photo[];
  age: number;
}
