import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonaService } from 'src/app/services/persona.service';
import { Persona } from '../models/persona';

@Component({
  selector: 'app-persona-registro',
  templateUrl: './persona-registro.component.html',
  styleUrls: ['./persona-registro.component.css']
})
export class PersonaRegistroComponent implements OnInit {
  persona: Persona;
  formGroup: FormGroup;

  //personasRegistradas: any;
  constructor(private personaService: PersonaService,private formBuilder: FormBuilder) { }
 
  ngOnInit() {
        this.buildForm();this.persona = new Persona();
      }

    private buildForm() {
          this.persona = new Persona();
          this.persona.identificacion = '';
          this.persona.nombre = '';
          this.persona.edad = 0;
          this.persona.pulsacion = 0;
          this.persona.sexo = '';
      
          this.formGroup = this.formBuilder.group({
            identificacion: [this.persona.identificacion, Validators.required],
            nombre: [this.persona.nombre, Validators.required],
            sexo: [this.persona.sexo, [Validators.required, this.validaSexo]],
            edad: [this.persona.edad, [Validators.required, Validators.min(1)]]
          });
        }
      private validaSexo(control: AbstractControl) {
         const sexo = control.value;
         if (sexo.toLocaleUpperCase() !== 'M' && sexo.toLocaleUpperCase() !== 'F') {
          return { validSexo: true, messageSexo: 'Sexo No Valido' 	};
         }
          return null;
        }
        get control() { 
          return this.formGroup.controls;
           }
          onSubmit() {
                if (this.formGroup.invalid) {
                  return;
                }
                this.add();
              }
            
  add() {
    this.persona = this.formGroup.value;

    /*
      this.personasRegistradas = this.obtenerPuls();
      this.personasRegistradas.push(this.persona);
      localStorage.setItem('pulsacion', JSON.stringify(this.personasRegistradas));
      console.log(this.persona);
      console.log(this.personasRegistradas);alert('listo');
       */

    this.personaService.post(this.persona).subscribe(p => {
      if (p != null) {
        alert('Persona creada!');
        this.persona = p;
      }
    });

  }/*
  CalculoPulsacion() {
    if (this.persona.sexo == "M") {

      this.persona.pulsacion = (210 - this.persona.edad) / 10;
    } else {

      this.persona.pulsacion = (220 - this.persona.edad) / 10;
    }

    return this.persona.pulsacion;

  }
  obtenerPuls() {
    if (localStorage.getItem('pulsacion') != null) {
      var auxiliar = localStorage.getItem('pulsacion');
      this.personasRegistradas = JSON.parse(auxiliar);
    } else {
      this.personasRegistradas = [];
    }
    return this.personasRegistradas;
  }*/

}
