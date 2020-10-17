import { ElementData } from './elementData';

export interface UserEditorForm {
  readonly firstName: string;
  readonly lastName: string;
  readonly dateOfBirth: string;
  readonly emails: ElementData[];
  readonly phones: ElementData[];
}
